﻿namespace PlaceRentalApp.API.Entities;

public class Comment : BaseEntity
{
    protected Comment() { }

    public Comment(int idUser, string comments)
        : base()
    {
        IdUser = idUser;
        Comments = comments;
    }

    public int IdUser { get; private set; }
    public string Comments { get; private set; }
}
