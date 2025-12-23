import os
import re
import json

# Find all test files and check for missing model properties
test_files = []
for root, dirs, files in os.walk("/workspace/LawMatics.SDK.Tests"):
    for file in files:
        if file.endswith("ClientTests.cs"):
            test_files.append(os.path.join(root, file))

# Common models that might be missing required properties
model_requirements = {
    "CustomContactType": ["CreatedAt", "UpdatedAt"],
    "TimeEntry": ["CreatedAt", "UpdatedAt"], 
    "TaskStatus": ["CreatedAt", "UpdatedAt"],
    "EventType": ["CreatedAt", "UpdatedAt"],
    "Subtask": ["CreatedAt", "UpdatedAt"],
    "MatterSubStatus": ["CreatedAt", "UpdatedAt"],
    "Expense": ["CreatedAt", "UpdatedAt"],
    "CustomEmail": ["CreatedAt", "UpdatedAt"],
    "Task": ["CreatedAt", "UpdatedAt"],
    "EmailAddress": ["CreatedAt", "UpdatedAt"],
    "Address": ["CreatedAt", "UpdatedAt"],
    "EventLocation": ["CreatedAt", "UpdatedAt"]
}

def fix_missing_properties(file_path, model_name, required_properties):
    """Fix missing properties for a specific model in a test file."""
    with open(file_path, 'r') as f:
        content = f.read()
    
    # Find all instances of the model creation
    pattern = rf'new {model_name}\s*\{{([^}}]+)\}}'
    matches = re.findall(pattern, content, re.MULTILINE | re.DOTALL)
    
    modified = False
    for match in matches:
        # Check if any required properties are missing
        for prop in required_properties:
            if prop not in match:
                # Add the missing property
                if match.strip().endswith('}'):
                    # Last property, add before closing brace
                    content = re.sub(
                        rf'(new {model_name}\s*\{{[^}}*?)([^}}]*?\}})',
                        rf'\1{match.strip()[:-1]},\n                        {prop} = DateTime.UtcNow\n{match.strip()[-1]}',
                        content,
                        flags=re.MULTILINE | re.DOTALL
                    )
                else:
                    # Add to existing properties
                    content = re.sub(
                        rf'(new {model_name}\s*\{{[^}}]*?)({match})',
                        rf'\1{match},\n                        {prop} = DateTime.UtcNow',
                        content,
                        flags=re.MULTILINE | re.DOTALL
                    )
                modified = True
    
    if modified:
        with open(file_path, 'w') as f:
            f.write(content)
        print(f"Fixed missing properties for {model_name} in {os.path.basename(file_path)}")
    
    return modified

# Process all test files
total_fixes = 0
for test_file in test_files:
    print(f"\nChecking {os.path.basename(test_file)}...")
    
    for model_name, required_props in model_requirements.items():
        if model_name in open(test_file, 'r').read():
            if fix_missing_properties(test_file, model_name, required_props):
                total_fixes += 1

print(f"\nTotal fixes applied: {total_fixes}")