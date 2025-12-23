#!/usr/bin/env python3

import os
import re

def fix_all_paged_response_tests():
    """Fix all PagedResponse tests to wrap responses in ApiResponse<>'"""
    
    # Find all test files that contain PagedResponse
    test_files = []
    for root, dirs, files in os.walk('/workspace/LawMatics.SDK.Tests'):
        for file in files:
            if file.endswith('Tests.cs') and file != 'CustomContactTypesClientTests.cs':  # Skip already fixed
                file_path = os.path.join(root, file)
                try:
                    with open(file_path, 'r') as f:
                        content = f.read()
                    if 'PagedResponse<' in content:
                        test_files.append(file_path)
                except Exception as e:
                    print(f"Error reading {file_path}: {e}")
    
    print(f"Found {len(test_files)} test files with PagedResponse to fix")
    
    total_fixes = 0
    
    for file_path in test_files:
        print(f"\nProcessing: {file_path}")
        
        try:
            with open(file_path, 'r') as f:
                content = f.read()
            
            original_content = content
            fixes_in_file = 0
            
            # Find lines with JsonSerializer.Serialize(expectedResponse, _jsonOptions)
            # and replace them with wrapped versions
            
            lines = content.split('\n')
            modified_lines = []
            
            i = 0
            while i < len(lines):
                line = lines[i]
                
                # Check if this line is the serialization line
                if 'JsonSerializer.Serialize(expectedResponse, _jsonOptions)' in line:
                    # Look backwards to find what expectedResponse is
                    expected_response_type = None
                    j = i - 1
                    while j >= 0:
                        prev_line = lines[j].strip()
                        if 'var expectedResponse = new ' in prev_line:
                            # Extract the type
                            match = re.search(r'new\s+(\w+(?:<[^>]+>)?)\s*{', prev_line)
                            if match:
                                expected_response_type = match.group(1)
                            break
                        j -= 1
                    
                    if expected_response_type:
                        # Determine the wrapper type
                        if 'PagedResponse' in expected_response_type:
                            # Extract the inner type for PagedResponse<T>
                            match = re.search(r'PagedResponse<([^>]+)>', expected_response_type)
                            if match:
                                inner_type = match.group(1)
                                wrapper_type = f'ApiResponse<PagedResponse<{inner_type}>>'
                            else:
                                wrapper_type = 'ApiResponse<PagedResponse<>>'
                        else:
                            wrapper_type = f'ApiResponse<{expected_response_type}>'
                        
                        # Insert the wrapper line before the serialization
                        indent = len(line) - len(line.lstrip())
                        wrapper_line = ' ' * indent + f'var apiResponse = new {wrapper_type} {{ Data = expectedResponse }};'
                        modified_lines.append(wrapper_line)
                        fixes_in_file += 1
                
                modified_lines.append(line)
                i += 1
            
            # Write back if changed
            if fixes_in_file > 0:
                content = '\n'.join(modified_lines)
                with open(file_path, 'w') as f:
                    f.write(content)
                print(f"  ✅ Fixed {fixes_in_file} PagedResponse responses in {file_path}")
                total_fixes += fixes_in_file
            else:
                print(f"  ⚪ No PagedResponse fixes needed for {file_path}")
                
        except Exception as e:
            print(f"  ❌ Error processing {file_path}: {e}")
    
    print(f"\n=== Summary ===")
    print(f"Total PagedResponse fixes applied: {total_fixes}")

if __name__ == "__main__":
    fix_all_paged_response_tests()