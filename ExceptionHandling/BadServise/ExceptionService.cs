using BadServise.Exceptions;

namespace BadServise
{
   
    public static class ExceptionService
    {
        public static void AttributeException()
        {
            throw new AttributeException("Attribute exception (for HandleError)");
        }

        public static void CustomAttributeException()
        {
            throw new CustomAttributeException("Custom attribute exception");
        }

        public static void ControllerLevelException()
        {
            throw new ControllerLevelException("Controller level error");
        }

        public static void LocalException()
        {
            throw new LocalException("Local exception");
        }

        public static void GlobalException()
        {
            throw new GlobalException("Global exception");
        }
    }
}
