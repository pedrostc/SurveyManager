using System;

namespace SurveyManager.Domain.Model.Implementation
{
    public abstract class Resposta : ModelBase
    {
        public bool EmBranco { get; protected set; }
        public int? Valor { get; protected set; }
    }
}