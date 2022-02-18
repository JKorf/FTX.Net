namespace FTX.Net.Objects.Models.Subaccounts
{
    /// <summary>
    /// Sub account info
    /// </summary>
    public class FTXSubaccount
    {
        /// <summary>
        /// Subaccount name
        /// </summary>
        public string Nickname { get; set; } = string.Empty;
        /// <summary>
        /// Whether the subaccount can be deleted
        /// </summary>
        public bool Deletable { get; set; }
        /// <summary>
        /// Whether the nickname of the subaccount can be changed
        /// </summary>
        public bool Editable { get; set; }
        /// <summary>
        /// Whether the subaccount was created for a competition
        /// </summary>
        public bool Competition { get; set; }
    }
}
