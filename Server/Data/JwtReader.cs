using System.Security.Claims;

namespace RentHome.Server.Data
{
    public class JwtReader
    {
        public static int GetUserId (ClaimsPrincipal user)
        {
            var identity = user.Identity as ClaimsIdentity; 

            if(identity == null )
            {
                return 0;
            }

            var claim = identity.Claims.FirstOrDefault(c => c.Type.ToLower() == "id");
            if( claim == null )
            {
                return 0;
            }

            int id;

            try
            {
                id = int.Parse(claim.Value);
    
            }
            catch (Exception)
            {
                return 0;
            }

            return id;
        }
    }
}
