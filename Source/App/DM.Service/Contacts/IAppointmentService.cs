﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;

namespace DM.Service.Contacts
{
    public interface IAppointmentService : IBaseService<Appointment>
    {
        List<Appointment> GetByDate(DateTime date);
        List<Appointment> Search(string request);
    }
}
