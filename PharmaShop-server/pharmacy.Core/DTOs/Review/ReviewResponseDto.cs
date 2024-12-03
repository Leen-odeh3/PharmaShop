﻿
namespace pharmacy.Core.DTOs.Review;
public class ReviewResponseDto
{
    public long Id { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CustomerName { get; set; } 
    public string ProductName { get; set; }  
}
