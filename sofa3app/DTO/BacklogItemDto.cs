namespace App.DTO
{
    public class BacklogItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string CurrentState { get; set; } = string.Empty;
    }
}