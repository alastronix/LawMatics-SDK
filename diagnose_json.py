#!/usr/bin/env python3

import subprocess
import json
import re

def run_test_with_diagnostics():
    """Run a specific failing test and capture detailed output"""
    
    # Run a specific failing test to see the exact error
    cmd = [
        'dotnet', 'test', 
        '--filter', 'CustomContactTypesClientTests.GetCustomContactTypesAsync_WithValidParameters_ReturnsCustomContactTypes',
        '--verbosity', 'detailed'
    ]
    
    try:
        result = subprocess.run(cmd, cwd='/workspace/LawMatics.SDK.Tests', 
                              capture_output=True, text=True, timeout=60)
        
        print("STDOUT:")
        print(result.stdout)
        print("\nSTDERR:")
        print(result.stderr)
        print(f"\nExit code: {result.returncode}")
        
        return result.stdout, result.stderr
    except subprocess.TimeoutExpired:
        print("Test timed out")
        return None, None
    except Exception as e:
        print(f"Error running test: {e}")
        return None, None

def examine_test_file():
    """Examine the test file structure"""
    
    with open('/workspace/LawMatics.SDK.Tests/CustomContactTypesClientTests.cs', 'r') as f:
        content = f.read()
    
    # Extract the failing test method
    test_start = content.find('GetCustomContactTypesAsync_WithValidParameters_ReturnsCustomContactTypes')
    if test_start == -1:
        print("Test method not found")
        return
    
    # Find the end of the method (next [Fact] or end of class)
    test_end = content.find('[Fact]', test_start + 1)
    if test_end == -1:
        test_end = content.find('}', content.rfind('}', 0, test_start))
    
    test_content = content[test_start:test_end]
    print("Test method content:")
    print(test_content)

if __name__ == "__main__":
    print("=== Running Diagnostic Tests ===")
    stdout, stderr = run_test_with_diagnostics()
    
    print("\n=== Examining Test File ===")
    examine_test_file()