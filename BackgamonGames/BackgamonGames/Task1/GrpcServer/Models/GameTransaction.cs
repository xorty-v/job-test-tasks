﻿namespace GrpcServer.Models;

public class GameTransaction
{
    public long Id { get; set; }

    public long SenderId { get; set; }
    public User Sender { get; set; }

    public long ReceiverId { get; set; }
    public User Receiver { get; set; }

    public double Amount { get; set; }
}