import os
import re

def fix_single_object_get_tests(file_path):
    """Fix single object GET tests by wrapping in ApiResponse<T>"""
    with open(file_path, 'r') as f:
        content = f.read()
    
    modified = False
    
    # Look for failing single object GET tests (pattern: *Async_WithValidId_Returns*)
    test_methods = re.findall(r'public async Task (\w+Async_WithValidId_Returns\w+)\(\)', content)
    
    for method_name in test_methods:
        # Find the test method
        method_pattern = rf'(\[Fact\]\s+public async Task {method_name}\(\)[^{{]*?\{{)(.*?)(\}}\s*\])'
        method_match = re.search(method_pattern, content, re.MULTILINE | re.DOTALL)
        
        if method_match:
            before, method_body, after = method_match.groups()
            
            # Check if this is a single object test that needs ApiResponse wrapper
            if 'new ApiResponse<' not in method_body and 'PagedResponse<' not in method_body:
                # Find the model type
                model_match = re.search(r'var expectedResponse = new (\w+)', method_body)
                if model_match:
                    model_type = model_match.group(1)
                    
                    # Replace to use ApiResponse wrapper
                    new_method_body = method_body.replace(
                        f'var expectedResponse = new {model_type}',
                        f'var expected{model_type} = new {model_type}'
                    ).replace(
                        'var jsonResponse = JsonSerializer.Serialize(expectedResponse, _jsonOptions);',
                        f'var apiResponse = new ApiResponse<{model_type}> {{ Data = expected{model_type} }};\n            var jsonResponse = JsonSerializer.Serialize(apiResponse, _jsonOptions);'
                    )
                    
                    # Update the full method
                    new_method = f'{before}{new_method_body}{after}'
                    content = content.replace(method_match.group(0), new_method)
                    modified = True
                    print(f"Fixed {method_name} in {os.path.basename(file_path)}")
    
    if modified:
        with open(file_path, 'w') as f:
            f.write(content)
    
    return modified

# Find test files that have single object tests
test_files = [
    "/workspace/LawMatics.SDK.Tests/EmailAddressesClientTests.cs",
    "/workspace/LawMatics.SDK.Tests/TasksClientTests.cs", 
    "/workspace/LawMatics.SDK.Tests/TimeEntriesClientTests.cs",
    "/workspace/LawMatics.SDK.Tests/ExpensesClientTests.cs",
    "/workspace/LawMatics.SDK.Tests/CustomEmailsClientTests.cs",
    "/workspace/LawMatics.SDK.Tests/TaskStatusesClientTests.cs",
    "/workspace/LawMatics.SDK.Tests/MatterSubStatusesClientTests.cs",
    "/workspace/LawMatics.SDK.Tests/SubtasksClientTests.cs",
    "/workspace/LawMatics.SDK.Tests/EventTypesClientTests.cs",
    "/workspace/LawMatics.SDK.Tests/CustomContactTypesClientTests.cs"
]

total_fixes = 0
for test_file in test_files:
    if os.path.exists(test_file):
        print(f"Checking {os.path.basename(test_file)}...")
        if fix_single_object_get_tests(test_file):
            total_fixes += 1

print(f"\nTotal files fixed: {total_fixes}")