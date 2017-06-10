﻿using System;
using rMedic.Helpers;

namespace rMedic.Models
{
    public class Manufacturer
    {
        #region Private members
        private int _id;
        private string _name;
        private string _address;
        private string _phone;
        #endregion

        #region Public Properties
        public int Id { get => _id; set => _id = value; }
        public string Name
        {
            get => _name;
            set => _name = (string.IsNullOrWhiteSpace(value)) ? throw new ArgumentNullException() : value;
        }
        public string Address
        {
            get => _address;
            set => _address = (string.IsNullOrWhiteSpace(value)) ? throw new ArgumentNullException() : value;
        }
        public string Phone
        {
            get => _phone.FormatPhoneNumber();
            set => _phone = (!value.PhoneNumberIsValid()) ? throw new ArgumentException() : value;
        }
        #endregion
    }
}