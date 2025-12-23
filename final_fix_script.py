import os
import re

def apply_api_response_wrapper(file_path):
    """Apply ApiResponse<T> wrapper pattern to single object tests"""
    with open(file_path, 'r') as f:
        content = f.read()
    
    modified = False
    
    # Find all test methods that follow the pattern *Async_WithValidId_Returns*
    test_pattern = r'(\[Fact\]\s+public async Task (\w+Async_WithValidId_Returns\w+)\(\)\s*\{)(.*?)(\})\s*(\[Fact\]|public class|\Z)'
    
    def fix_test_method(match):
        before_brace, method_name, method_body, after_brace = match.groups()
        
        # Skip if already has ApiResponse or PagedResponse
        if 'ApiResponse<' in method_body or 'PagedResponse<' in method_body:
            return match.group(0)
        
        # Find model type and variable name
        model_match = re.search(r'var expected(\w*) = new (\w+)', method_body)
        if not model_match:
            return match.group(0)
        
        var_suffix, model_type = model_match.groups()
        var_name = f"expected{model_type}" if not var_suffix else f"expected{var_suffix}"
        
        # Replace the method body
        new_body = method_body.replace(
            f'var expected{var_suffix} = new {model_type}',
            f'var {var_name} = new {model_type}'
        )
        
        # Replace JSON serialization
        new_body = re.sub(
            r'var jsonResponse = JsonSerializer\.Serialize\((\w+), _jsonOptions\)',
            lambda m: f'var apiResponse = new ApiResponse<{model_type}> {{ Data = {var_name} }};\n            var jsonResponse = JsonSerializer.Serialize(apiResponse, _jsonOptions)',
            new_body
        )
        
        modified = True
        print(f"Fixed {method_name} in {os.path.basename(file_path)}")
        
        return f'{before_brace}{new_body}{after_brace}'
    
    # Apply the fix
    new_content = re.sub(test_pattern, fix_test_method, content, flags=re.MULTILINE | re.DOTALL)
    
    if modified and new_content != content:
        with open(file_path, 'w') as f:
            f.write(new_content)
    
    return modified

# Process test files
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
        print(f"Processing {os.path.basename(test_file)}...")
        if apply_api_response_wrapper(test_file):
            total_fixes += 1

print(f"\nTotal files modified: {total_fixes}")