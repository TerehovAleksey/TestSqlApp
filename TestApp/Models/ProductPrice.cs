﻿namespace TestApp.Models;

public class ProductPrice
{
    public int Id { get; set; }
    public Guid Article { get; set; }
    public decimal Price { get; set; }
}