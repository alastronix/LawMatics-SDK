import os
import re

# Find all test files
test_files = []
for root, dirs, files in os.walk("/workspace/LawMatics.SDK.Tests"):
    for file in files:
        if file.endswith("ClientTests.cs") and file != "AddressesClientTests.cs":
            test_files.append(os.path.join(root, file))

def fix_single_object_tests(file_path):
    """Fix single object GET tests to use ApiResponse<T> wrapper"""
    with open(file_path, 'r') as f:
        content = f.read()
    
    modified = False
    
    # Pattern to find single object test methods that are failing
    # Look for tests that create single object responses but don't wrap in ApiResponse
    single_object_pattern = r'(\[Fact\]\s+public async Task \w+Async_WithValidId_Returns\w+\(\)\s*{\s*// Arrange[^}]*?var expectedResponse = new \w+\{[^}]*?\};\s*var jsonResponse = JsonSerializer\.Serialize\(expectedResponse, _jsonOptions\);)'
    
    # Find all matches
    matches = re.finditer(single_object_pattern, content, re.MULTILINE | re.DOTALL)
    
    for match in matches:
        test_code = match.group(1)
        
        # Check if this is a single object test that doesn't use ApiResponse
        if 'ApiResponse<' not in test_code and 'new PagedResponse<' not in test_code:
            # Extract the model type
            model_match = re.search(r'var expectedResponse = new (\w+)\s*\{', test_code)
            if model_match:
                model_type = model_match.group(1)
                
                # Replace the test code to wrap in ApiResponse
                old_code = test_code
                new_code = test_code.replace(
                    f'var expectedResponse = new {model_type}',
                    f'var expected{model_type} = new {model_type}'
                ).replace(
                    'var jsonResponse = JsonSerializer.Serialize(expectedResponse, _jsonOptions);',
                    f'var apiResponse = new ApiResponse<{model_type}> {{ Data = expected{model_type} }};\n            var jsonResponse = JsonSerializer.Serialize(apiResponse, _jsonOptions);'
                )
                
                content = content.replace(old_code, new_code)
                modified = True
                print(f"Fixed single object test in {os.path.basename(file_path)}")
    
    if modified:
        with open(file_path, 'w') as f:
            f.write(content)
    
    return modified

def fix_paged_response_tests(file_path):
    """Fix PagedResponse tests to ensure they have complete PaginationInfo"""
    with open(file_path, 'r') as f:
        content = f.read()
    
    modified = False
    
    # Pattern to find PagedResponse tests
    paged_pattern = r'new PaginationInfo\s*\{([^}]+)\}'
    
    def replace_pagination(match):
        pagination_content = match.group(1)
        
        # Check if required properties are missing
        if 'TotalPages' not in pagination_content or 'HasNext' not in pagination_content:
            # Add missing properties
            if pagination_content.strip().endswith(','):
                pagination_content = pagination_content.rstrip(',')
            
            # Add missing properties
            missing_props = []
            if 'TotalPages' not in pagination_content:
                missing_props.append('TotalPages = 1')
            if 'HasNext' not in pagination_content:
                missing_props.append('HasNext = false')
            
            if missing_props:
                new_pagination = pagination_content + ',\n                        ' + ',\n                        '.join(missing_props)
                print(f"Added missing pagination properties in {os.path.basename(file_path)}")
                return f'new PaginationInfo\r\n{{ {new_pagination}\r\n                        }}'
        
        return match.group(0)
    
    content = re.sub(paged_pattern, replace_pagination, content, flags=re.MULTILINE | re.DOTALL)
    
    # Check if content was modified by comparing length
    if content != open(file_path, 'r').read():
        with open(file_path, 'w') as f:
            f.write(content)
        modified = True
    
    return modified

# Process all test files
total_fixes = 0
for test_file in test_files:
    print(f"\nChecking {os.path.basename(test_file)}...")
    
    if fix_single_object_tests(test_file):
        total_fixes += 1
    
    if fix_paged_response_tests(test_file):
        total_fixes += 1

print(f"\nTotal files fixed: {total_fixes}")