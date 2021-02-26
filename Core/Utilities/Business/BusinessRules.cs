using Core.Utilitiess.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilitiess.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                }
            }

            return null;
        }

    }
}
