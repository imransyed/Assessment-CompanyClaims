﻿namespace DataLayer.Entities;

public class Claim
{
    public string Ucr { get; set; }
    public int CompanyId { get; set; }
    public DateTime ClaimDate { get; set; }
    public DateTime LossDate { get; set; }
    public string AssuredName { get; set; }
    public decimal IncurredLoss { get; set; }
    public int ClaimTypeId { get; set; }
    public string ClaimTypeName { get; set; }
    public bool Closed { get; set; }
}


