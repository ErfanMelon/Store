﻿using System.Security.Claims;

namespace Store.EndPoint.Tools
{
    public class ClaimTool
    {
        public static long? GetUserId(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;

                if (claimsIdentity.FindFirst(ClaimTypes.NameIdentifier) != null)
                {
                    long userId = long.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    return userId;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                return null;
            }

        }
        public static string? GetUserRole(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;

                if (claimsIdentity.FindFirst(ClaimTypes.Role) != null)
                {
                    return claimsIdentity.FindFirst(ClaimTypes.Role).Value;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                return null;
            }

        }

    }
}
