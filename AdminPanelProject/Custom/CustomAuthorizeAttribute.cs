//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc.Filters;
//using System.IdentityModel.Tokens.Jwt;

//namespace AdminPanelProject.Custom
//{
//    public class CustomAuthorizeAttribute : AuthorizeAttribute
//    {
//        private readonly string _role;
//        public CustomAuthorizeAttribute(string Role)
//        {
//            _role = Role;

//        }

//        public void OnActionExecuted(ActionExecutedContext context)
//        {
//            var req = context.HttpContext.Request.Headers.Authorization;
//            var auth = req.Where(w => w.Contains("Bearer")).FirstOrDefault().Split(" ");
//            string token = "";
//            var handler = new JwtSecurityTokenHandler();
//            if (auth.Length == 2 && auth[0] == "Bearer")
//            {
//                token = auth[1];
//            }

//            var decodedToken = handler.ReadJwtToken(token);
//            var claims = decodedToken.Claims.ToList();
//            var isOk = false;
//            foreach (var claim in claims)
//            {
//                if (claim.Type == "role" && claim.Value == _role)
//                {
//                    isOk = true;
//                }
//            }
//            if (!isOk)
//            {
//                return;
//            }
//        }

//        public void OnActionExecuting(ActionExecutingContext context)
//        {
//            var req = context.HttpContext.Request.Headers.Authorization;
//            var auth = req.Where(w => w.Contains("Bearer")).FirstOrDefault().Split(" ");
//        }

//        public void OnAuthorization(AuthorizationFilterContext context)
//        {
//            var req = context.HttpContext.Request.Headers.Authorization;
//            var auth = req.Where(w => w.Contains("Bearer")).FirstOrDefault().Split(" ");
//            string token = "";
//            var handler = new JwtSecurityTokenHandler();
//            if (auth.Length == 2 && auth[0] == "Bearer")
//            {
//                token = auth[1];
//            }

//            var decodedToken = handler.ReadJwtToken(token);
//            var claims = decodedToken.Claims.ToList();
//            var isOk = false;
//            foreach (var claim in claims)
//            {
//                if (claim.Type == "role" && claim.Value == _role)
//                {
//                    isOk = true;
//                }
//            }
//        }
//    }
//}
