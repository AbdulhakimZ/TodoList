﻿namespace TodoList.Models
{
#nullable disable
    public class TodoItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsComplete { get; set; }
    }
}
