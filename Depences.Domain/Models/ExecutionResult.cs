using Depences.Domain.Enums;

namespace Depences.Domain.Models
{
    public class ExecutionResult
    {
        public ExecutionStatus? Status { get; set; }
        public Origin? Origine { get; set; }
        public string? File { get; set; }
        public string? Function { get; set; }
        public int? ErrorCode { get; set; }
        public string? Message { get; set; } = "";
        public string? ActionName { get; set; } = "";
        public ExecutionResult()
        {
            Status = ExecutionStatus.Starting;
        }
        public ExecutionResult(ExecutionStatus status, string message = "", string actionName = "")
        {
            Status = status;
            Message = message;
            ActionName = actionName;
        }

        public ExecutionResult(ExecutionStatus status, Origin application, string? message)
        {
            this.Status = status;
            this.Origine = application;
            this.Message = message;
        }

        public bool IsSuccess => Status == ExecutionStatus.Success || Status == ExecutionStatus.Warning;
        public string GenerateLogMessag()
        {
            if (Status == ExecutionStatus.Error)
                return $" ErrorCode: {ErrorCode}, Origine: {Origine}, File: {File}, Func: {Function}, Dtails: {Message} #";
            return string.Empty;
        }
    }
}
