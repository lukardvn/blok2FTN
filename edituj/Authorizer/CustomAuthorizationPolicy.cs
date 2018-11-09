﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Policy;
using System.IdentityModel.Claims;
using System.Security.Principal;

namespace Authorizer {
    public class CustomAuthorizationPolicy : IAuthorizationPolicy {
        private string id;
        private object locker = new object();

        public CustomAuthorizationPolicy() {
            this.id = Guid.NewGuid().ToString();
        }

        public string Id {
            get { return this.id; }
        }

        public ClaimSet Issuer {
            get { return ClaimSet.System; }
        }

        public bool Evaluate(EvaluationContext evaluationContext, ref object state) {
            object list;

            if (!evaluationContext.Properties.TryGetValue("Identities", out list))  //provera da li je prosla windows autorizacija
            {
                return false;
            }

            IList<IIdentity> identities = list as IList<IIdentity>;     //provera da li je prosla windows autorizacija
            if (list == null || identities.Count <= 0) {
                return false;
            }
            
            //ako je proslo, kreira se principal
            evaluationContext.Properties["Principal"] = GetPrincipal(identities[0]);
            return true;
        }

        protected virtual IPrincipal GetPrincipal(IIdentity identity) {
            lock (locker)
            {
                IPrincipal principal = null;
                WindowsIdentity windowsIdentity = identity as WindowsIdentity;
                IIdentity x059Identity = identity;
                //X509Identity windowsIdentity = identity as X05Identity;
                //System.IdentityModel.Claims.

                if (windowsIdentity != null)
                {
                    //Audit.AuthenticationSuccess(windowsIdentity.Name);
                    principal = new MyPrincipal(windowsIdentity);
                }
                else if (x059Identity != null)
                {
                    principal = new MyPrincipal(x059Identity);
                }

                return principal;
            }
        }
    }
}
