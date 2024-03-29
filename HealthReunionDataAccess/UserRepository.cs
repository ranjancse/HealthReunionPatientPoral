﻿using HealthReunionDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

/// <summary>
/// Summary description for UserRepository
/// </summary>
public class UserRepository
{
	public UserRepository()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public User GetUserById(int patientId)
    {
        using (var dataContext = new HealthReunionEntities())
        {
            var user = (from u in dataContext.Users
                        where u.PatientId == patientId
                        select u).FirstOrDefault();
            return user;
        }
    }

    public void AddUser(User user)
    {
        using (var dataContext = new HealthReunionEntities())
        {
            user.Password = EncryptDecrypt.EncryptData(user.Password, EncryptDecrypt.ReadCert());
            // Add user entity
            dataContext.Users.Add(user);
            // Save changes so that it will insert records into database.
            dataContext.SaveChanges();           
        }
    }

    public void UpdatePassword(User user)
    {
        using (var dataContext = new HealthReunionEntities())
        {
            var original = dataContext.Users.Find(user.UserId);
            if (original != null)
            {
                original.Password = EncryptDecrypt.EncryptData(user.Password, EncryptDecrypt.ReadCert());
            }
            dataContext.SaveChanges();
        }
    }
}