namespace Shared.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Shared.Interface;

    /// <summary>
    /// Define a QueueProcess class
    /// </summary>
    [Table("QueueProcesses")]
    public class QueueProcess : IModel
    {
        /// <summary>
        /// Gets or sets Queue's id
        /// </summary>
        /// <returns>Return id</returns>
        [Key]
        [Required(AllowEmptyStrings = false)]
        public string QueueId { get; set; }

        /// <summary>
        /// Gets or sets Process's id
        /// </summary>
        /// <returns>Return id</returns>
        public string ProcessId { get; set; }
    }
}
