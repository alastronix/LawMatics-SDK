#!/usr/bin/env python3

import os
import subprocess
import re

def fix_single_object_tests():
    """Fix all remaining single object tests to wrap responses in ApiResponse<>'"""
    
    # List of files that need fixing for single objects
    files_to_check = [
        'TaskStatusesClientTests.cs',
        'EventTypesClientTests.cs', 
        'MatterSubStatusesClientTests.cs',
        'SubtasksClientTests.cs',
        'ExpensesClientTests.cs',
        'TasksClientTests.cs',
        'TimeEntriesClientTests.cs'
    ]
    
    # Object types for each file
    object_types = {
        'TaskStatusesClientTests.cs': 'LawMatics.SDK.Models.TaskStatus',
        'EventTypesClientTests.cs': 'EventType',
        'MatterSubStatusesClientTests.cs': 'MatterSubStatus', 
        'SubtasksClientTests.cs': 'Subtask',
        'ExpensesClientTests.cs': 'Expense',
        'TasksClientTests.cs': 'TaskItem',
        'TimeEntriesClientTests.cs': 'TimeEntry'
    }
    
    total_fixes = 0
    
    for filename in files_to_check:
        filepath = f'/workspace/LawMatics.SDK.Tests/{filename}'
        if os.path.exists(filepath):
            print(f"Checking {filename}...")
            
            # Find lines that still have direct expectedResponse serialization (not already fixed)
            result = subprocess.run([
                'grep', '-n', 'JsonSerializer.Serialize(expectedResponse, _jsonOptions)', filepath
            ], capture_output=True, text=True)
            
            if result.returncode == 0:
                lines = result.stdout.strip().split('\n')
                for line in lines:
                    if line.strip():
                        line_num = int(line.split(':')[0])
                        print(f"  Found unfixed serialization at line {line_num}")
                        
                        # Check what type this is by looking backwards
                        check_result = subprocess.run([
                            'sed', '-n', f'1,{line_num}p', filepath
                        ], capture_output=True, text=True)
                        
                        content = check_result.stdout
                        
                        # Look for the expectedResponse declaration
                        if 'new PagedResponse<' in content:
                            print(f"  -> Skipping - this is a PagedResponse (already fixed)")
                        elif 'var expectedResponse = new ' in content:
                            # This is a single object - fix it
                            object_type = object_types.get(filename, 'Unknown')
                            print(f"  -> Single object {object_type} found, adding ApiResponse wrapper")
                            
                            # Fix the line
                            replacement = f'var apiResponse = new ApiResponse<{object_type}> {{ Data = expectedResponse }};\\n               var jsonResponse = JsonSerializer.Serialize(apiResponse, _jsonOptions);'
                            subprocess.run([
                                'sed', '-i', f'{line_num}s/var jsonResponse = JsonSerializer.Serialize(expectedResponse, _jsonOptions);/{replacement}/', filepath
                            ])
                            total_fixes += 1
                        else:
                            print(f"  -> Could not determine expectedResponse type")
            else:
                print(f"  No unfixed expectedResponse serialization found")
        else:
            print(f"File {filename} not found")
    
    print(f"\n=== Summary ===")
    print(f"Total single object fixes applied: {total_fixes}")

if __name__ == "__main__":
    fix_single_object_tests()