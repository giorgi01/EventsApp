using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EventsApp.Mvc.Helpers
{
    public static class ControllerHelper
    {
        public static async Task ModelStateAddErrorsAsync(ModelStateDictionary modelState, HttpResponseMessage response)
        {
            var errorsDict = await ParseErrorsAsync(response);
            foreach (var field in errorsDict.Keys)
            {
                foreach (var error in errorsDict[field])
                {
                    modelState.AddModelError(field, error.ToString());
                }
            }
        }

        private static async Task<Dictionary<string, IEnumerable>> ParseErrorsAsync(HttpResponseMessage response)
        {
            var content = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
            var errors = content["errors"].ToString();
            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, IEnumerable>>(errors);
        }

    }
}
