﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Interfaces;

namespace WebStore.WebAPI.Controllers
{
    [Route(WebAPIAddresses.Values)]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static List<string> __Values = Enumerable
            .Range(1, 10)
            .Select(i => $"Value-{i}")
            .ToList();

        [HttpGet]
        public IActionResult Get() => Ok(__Values);

        [HttpGet("count")]
        public IActionResult Count() => Ok(__Values.Count);

        [HttpGet("{index}")]
        [HttpGet("index[[{index}]]")]
        public ActionResult<string> GetByIndex(int index)
        {
            if (index < 0)
                return BadRequest();
            if (index > __Values.Count)
                return NotFound();

            return __Values[index];
        }

        [HttpPost]
        [HttpPost("add")]
        public IActionResult Add(string str)
        {
            __Values.Add(str);
            var index = __Values.Count - 1;
            return CreatedAtAction(nameof(GetByIndex), new { index });

        }

        [HttpPut("{index}")]
        [HttpPut("edit/{index}")]
        public ActionResult Replace(int index, string NewStr)
        {
            if (index < 0)
                return BadRequest();
            if (index > __Values.Count)
                return NotFound();

            __Values[index] = NewStr;
            return Ok();
        }

        [HttpDelete("{index}")]
        public ActionResult Delete(int index, string NewStr)
        {
            if (index < 0)
                return BadRequest();
            if (index > __Values.Count)
                return NotFound();

            __Values.RemoveAt(index);
            return Ok();
        }
    }
}
