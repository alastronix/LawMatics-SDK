#!/usr/bin/env python3

import os
import re

def fix_paged_response_tests():
    """Fix all PagedResponse tests to wrap responses in ApiResponse<>'"""
    
    # Find all test files that contain PagedResponse
    test_files = []
    for root, dirs, files in os.walk('/workspace/LawMatics.SDK.Tests'):
        for file in files:
            if file.endswith('Tests.cs'):
                file_path = os.path.join(root, file)
                try:
                    with open(file_path, 'r') as f:
                        content = f.read()
                    if 'PagedResponse<' in content:
                        test_files.append(file_path)
                except Exception as e:
                    print(f"Error reading {file_path}: {e}")
    
    print(f"Found {len(test_files)} test files with PagedResponse")
    
    for file_path in test_files:
        print(f"\nProcessing: {file_path}")
        
        try:
            with open(file_path, 'r') as f:
                content = f.read()
            
            original_content = content
            
            # Pattern to find PagedResponse variable declarations and their usage in JSON serialization
            # We need to find cases like:
            # var expectedResponse = new PagedResponse<ModelType> { ... };
            # var jsonResponse = JsonSerializer.Serialize(expectedResponse, _jsonOptions);
            
            # First, find all PagedResponse variable declarations
            paged_response_pattern = r'(\s+var\s+(\w+)\s*=\s*new\s+PagedResponse<[^>]+>\s*\{[^}]*\}\s*;)'
            
            def replace_paged_response(match):
                indent = match.group(1)
                var_name = match.group(2)
                full_declaration = match.group(0)
                
                # Check if this is already wrapped in ApiResponse
                # Look ahead to see if the next few lines wrap it in ApiResponse
                lines_after_match = content[match.end():match.end() + 500]
                if 'ApiResponse<' in lines_after_match and var_name in lines_after_match:
                    return full_declaration  # Already wrapped
                
                # Create the wrapped version
                wrapped_declaration = f'{indent}var apiResponse = new ApiResponse<PagedResponse<>> {{ Data = {var_name} }};\n'
                wrapped_declaration += f'{indent}var jsonResponse = JsonSerializer.Serialize(apiResponse, _jsonOptions);'
                
                return wrapped_declaration
            
            # Apply the replacement
            content = re.sub(paged_response_pattern, replace_paged_response, content, flags=re.DOTALL)
            
            # Now we need to remove the original serialization line and update variable references
            # Pattern to find and remove: var jsonResponse = JsonSerializer.Serialize(expectedResponse, _jsonOptions);
            serialization_pattern = r'\s+var\s+jsonResponse\s*=\s*JsonSerializer\.Serialize\([^,]+,\s*_jsonOptions\)\s*;'
            content = re.sub(serialization_pattern, '', content)
            
            # Write back if changed
            if content != original_content:
                with open(file_path, 'w') as f:
                    f.write(content)
                print(f"  ✅ Updated {file_path}")
            else:
                print(f"  ⚪ No changes needed for {file_path}")
                
        except Exception as e:
            print(f"  ❌ Error processing {file_path}: {e}")

if __name__ == "__main__":
    fix_paged_response_tests()