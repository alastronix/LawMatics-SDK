#!/usr/bin/env python3

import os
import subprocess

def quick_fix_remaining():
    """Quick fix for remaining PagedResponse tests"""
    
    # List of files that need fixing
    files_to_fix = [
        'ExpensesClientTests.cs',
        'MatterSubStatusesClientTests.cs', 
        'TaskStatusesClientTests.cs',
        'EventTypesClientTests.cs',
        'SubtasksClientTests.cs',
        'EventLocationsClientTests.cs',
        'TimeEntriesClientTests.cs',
        'EmailAddressesClientTests.cs',
        'TasksClientTests.cs'
    ]
    
    for filename in files_to_fix:
        filepath = f'/workspace/LawMatics.SDK.Tests/{filename}'
        if os.path.exists(filepath):
            print(f"Fixing {filename}...")
            
            # Find all JsonSerializer.Serialize lines with expectedResponse
            result = subprocess.run([
                'grep', '-n', 'JsonSerializer.Serialize(expectedResponse, _jsonOptions)', filepath
            ], capture_output=True, text=True)
            
            if result.returncode == 0:
                lines = result.stdout.strip().split('\n')
                for line in lines:
                    if line.strip():
                        line_num = line.split(':')[0]
                        print(f"  Found serialization at line {line_num}")
                        
                        # Check what type expectedResponse is by looking backwards
                        check_result = subprocess.run([
                            'sed', '-n', f'1,{int(line_num)}p', filepath
                        ], capture_output=True, text=True)
                        
                        content = check_result.stdout
                        
                        # Look for the expectedResponse declaration
                        if 'new PagedResponse<' in content:
                            # This is a PagedResponse - fix it
                            print(f"  -> PagedResponse found, adding ApiResponse wrapper")
                            replacement = f'var apiResponse = new ApiResponse<PagedResponse<>> {{ Data = expectedResponse }};\\n               var jsonResponse = JsonSerializer.Serialize(apiResponse, _jsonOptions);'
                            subprocess.run([
                                'sed', '-i', f'{line_num}s/var jsonResponse = JsonSerializer.Serialize(expectedResponse, _jsonOptions);/{replacement}/', filepath
                            ])
                        elif 'var expectedResponse = new ' in content:
                            # This is a single object - get the type
                            import re
                            match = re.search(r'var expectedResponse = new (\w+)', content)
                            if match:
                                object_type = match.group(1)
                                print(f"  -> Single object {object_type} found, adding ApiResponse wrapper")
                                replacement2 = f'var apiResponse = new ApiResponse<{object_type}> {{ Data = expectedResponse }};\\n               var jsonResponse = JsonSerializer.Serialize(apiResponse, _jsonOptions);'
                                subprocess.run([
                                    'sed', '-i', f'{line_num}s/var jsonResponse = JsonSerializer.Serialize(expectedResponse, _jsonOptions);/{replacement2}/', filepath
                                ])
            else:
                print(f"  No expectedResponse serialization found")
        else:
            print(f"File {filename} not found")

if __name__ == "__main__":
    quick_fix_remaining()