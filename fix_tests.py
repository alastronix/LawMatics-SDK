import os
import re

# List of test files that need the _jsonOptions fix
test_files = [
    "EventTypesClientTests.cs",
    "SubtasksClientTests.cs", 
    "MatterSubStatusesClientTests.cs",
    "ExpensesClientTests.cs",
    "CustomEmailsClientTests.cs",
    "TasksClientTests.cs",
    "TimeEntriesClientTests.cs",
    "EmailAddressesClientTests.cs",
    "AddressesClientTests.cs",
    "EventLocationsClientTests.cs",
    "EmailCampaignStatsClientTests.cs",
    "CommentsClientTests.cs",
    "LawMaticsClientTests.cs"
]

base_path = "/workspace/LawMatics.SDK.Tests"

# JSON options initialization code
json_options_field = "        private readonly JsonSerializerOptions _jsonOptions;\n"
json_options_init = """            
            // Initialize JSON options to match BaseClient
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };"""

for test_file in test_files:
    file_path = os.path.join(base_path, test_file)
    
    if not os.path.exists(file_path):
        print(f"File not found: {file_path}")
        continue
        
    print(f"Processing {test_file}...")
    
    with open(file_path, 'r') as f:
        content = f.read()
    
    # Check if _jsonOptions already exists
    if "_jsonOptions" in content:
        print(f"  Skipping {test_file} - _jsonOptions already exists")
        continue
    
    # Find the location to add the _jsonOptions field (after _httpClient field)
    http_client_pattern = r'(\s+private readonly HttpClient _httpClient;)'
    match = re.search(http_client_pattern, content)
    
    if match:
        # Add the _jsonOptions field after _httpClient field
        content = re.sub(http_client_pattern, r'\1\n' + json_options_field, content)
        
        # Find the constructor end to add JSON options initialization
        constructor_pattern = r'(\s+BaseAddress = new Uri\(_options\.BaseUrl\)\s*\}\s*;)'
        match = re.search(constructor_pattern, content)
        
        if match:
            # Add JSON options initialization after constructor setup
            content = re.sub(constructor_pattern, r'\1' + json_options_init, content)
            
            # Update JsonSerializer.Serialize calls to use _jsonOptions
            content = re.sub(r'JsonSerializer\.Serialize\(([^,)]+)\)', r'JsonSerializer.Serialize(\1, _jsonOptions)', content)
            
            # Write the updated content back to the file
            with open(file_path, 'w') as f:
                f.write(content)
            
            print(f"  Fixed {test_file}")
        else:
            print(f"  Could not find constructor pattern in {test_file}")
    else:
        print(f"  Could not find _httpClient field pattern in {test_file}")

print("Done!")