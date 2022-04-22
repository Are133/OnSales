using System;

namespace OnSales.Web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string DuplicatedError => $"Este registro ya fue registrado";

    }
}
