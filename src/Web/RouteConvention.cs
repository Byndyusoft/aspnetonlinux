namespace Web
{
    using System.Linq;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.ApplicationModels;

    public class RouteConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _centralPrefix;

        public RouteConvention(string preffix)
        {
            _centralPrefix = new AttributeRouteModel(new RouteAttribute(
                (string.IsNullOrEmpty(preffix) == false
                    ? preffix + "/"
                    : "") + "[controller]"));
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
                if (controller.AttributeRoutes.Any())
                    for (var i = 0; i < controller.AttributeRoutes.Count; i++)
                        controller.AttributeRoutes[i] = AttributeRouteModel.CombineAttributeRouteModel(_centralPrefix,
                            controller.AttributeRoutes[i]);
                else
                    controller.AttributeRoutes.Add(_centralPrefix);
        }
    }
}