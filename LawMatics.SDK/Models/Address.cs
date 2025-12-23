using System.Text.Json.Serialization;

namespace LawMatics.SDK.Models
{
    /// <summary>
    /// Represents an address in the LawMatics system.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the street address line 1.
        /// </summary>
        [JsonPropertyName("street1")]
        public string? Street1 { get; set; }

        /// <summary>
        /// Gets or sets the street address line 2.
        /// </summary>
        [JsonPropertyName("street2")]
        public string? Street2 { get; set; }

        /// <summary>
        /// Gets or sets the city name.
        /// </summary>
        [JsonPropertyName("city")]
        public string? City { get; set; }

        /// <summary>
        /// Gets or sets the state or province.
        /// </summary>
        [JsonPropertyName("state")]
        public string? State { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        /// <summary>
        /// Gets or sets the county.
        /// </summary>
        [JsonPropertyName("county")]
        public string? County { get; set; }

        /// <summary>
        /// Gets or sets the full address formatted as a single string.
        /// </summary>
        [JsonIgnore]
        public string FullAddress
        {
            get
            {
                var parts = new List<string>();
                
                if (!string.IsNullOrEmpty(Street1))
                    parts.Add(Street1);
                
                if (!string.IsNullOrEmpty(Street2))
                    parts.Add(Street2);
                
                var cityStateZip = string.Join(" ", new[]
                {
                    City,
                    State,
                    PostalCode
                }.Where(s => !string.IsNullOrEmpty(s)));
                
                if (!string.IsNullOrEmpty(cityStateZip))
                    parts.Add(cityStateZip);
                
                if (!string.IsNullOrEmpty(Country))
                    parts.Add(Country);
                
                return string.Join(", ", parts);
            }
        }

        /// <summary>
        /// Returns a formatted string representation of the address.
        /// </summary>
        /// <returns>The formatted address string.</returns>
        public override string ToString()
        {
            return FullAddress;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current address.
        /// </summary>
        /// <param name="obj">The object to compare with the current address.</param>
        /// <returns>True if the specified object is equal to the current address; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is Address other)
            {
                return string.Equals(Street1, other.Street1, StringComparison.OrdinalIgnoreCase) &&
                       string.Equals(Street2, other.Street2, StringComparison.OrdinalIgnoreCase) &&
                       string.Equals(City, other.City, StringComparison.OrdinalIgnoreCase) &&
                       string.Equals(State, other.State, StringComparison.OrdinalIgnoreCase) &&
                       string.Equals(PostalCode, other.PostalCode, StringComparison.OrdinalIgnoreCase) &&
                       string.Equals(Country, other.Country, StringComparison.OrdinalIgnoreCase) &&
                       string.Equals(County, other.County, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        /// <summary>
        /// Returns the hash code for this address.
        /// </summary>
        /// <returns>A hash code for the current address.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(
                Street1?.ToUpperInvariant(),
                Street2?.ToUpperInvariant(),
                City?.ToUpperInvariant(),
                State?.ToUpperInvariant(),
                PostalCode?.ToUpperInvariant(),
                Country?.ToUpperInvariant(),
                County?.ToUpperInvariant()
            );
        }
    }
}