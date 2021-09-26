using Calendar.Core.Domain;

namespace Calendar.AplicationService.Commands.EventItemAggregate.UpdateEventItem
{
    public class UpdateEventItemDto
    {
        public static UpdateEventItemDto Success(UpdateEventItemCommand data) => new() { Data = data, IsSuccess = true };

        public static UpdateEventItemDto Faild() => new() { Data = null, IsSuccess = false };


        public bool IsSuccess { get; set; }
        public UpdateEventItemCommand Data { get; set; }
    }
}
