﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;
using Manager;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;

namespace Readers
{
    class ReaderProxy : ChannelFactory<IMainService>, IMainService, IDisposable
    {
        IMainService factory;

        public ReaderProxy(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            factory = this.CreateChannel();
        }

        public bool CreateDB(string name)
        {
            bool retVal = false;
            try
            {
                factory.CreateDB(name);
                retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public bool DeleteDB(string name)
        {
            bool retVal = false;
            try
            {
                factory.DeleteDB(name);
                retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public bool EditDB(string name, string txt)
        {
            //throw new NotImplementedException();
            bool retVal = false;
            try
            {
                factory.EditDB(name, txt);
                retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public bool MaxIncomeByCountry()
        {
            bool retVal = false;
            try
            {
                throw new NotImplementedException();    //OVA FALI JOS
                //retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public bool MedianMonthlyIncomeByCity(string city)
        {
            bool retVal = false;
            try
            {
                factory.MedianMonthlyIncomeByCity(city);
                retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public bool MedianMonthlyIncome(string country, int year)
        {
            bool retVal = false;
            try
            {
                factory.MedianMonthlyIncome(country, year);
                retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public bool ReadDB(string name)
        {
            bool retVal = false;
            try
            {
                factory.ReadDB(name);
                retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public bool WriteDB(string name, string txt)
        {
            bool retVal = false;
            try
            {
                factory.WriteDB(name, txt);
                retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }
    }
}
