﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Okapi.Common;
using Okapi.Extensions;
using Okapi.Report;
using Serilog;
using SeriLogLogger = Serilog.Core.Logger;

namespace Okapi.Support.Report.Text
{
    public class ReportFormatter : IReportFormatter
    {
        private static SeriLogLogger logger;

        public ReportFormatter()
        {
            Session session = Session.Instance;
            string reportDirectory = session.ReportDirectory;
            string reportFileName = $"{reportDirectory}{Path.DirectorySeparatorChar}OkapiReport_{session.StartDateTime.GetTimestamp()}.txt";
            logger = new LoggerConfiguration()
                .WriteTo
                .File(reportFileName).CreateLogger();
        }

        public void Run(TestCase testCase)
        {
            StringBuilder reportStringBuilder = new StringBuilder();
            reportStringBuilder.Append($"TEST CASE: {testCase.Method.Name}");

            NewLineAndTab(reportStringBuilder);
            reportStringBuilder.Append($"RESULT: {testCase.Result}");

            if (testCase.DurationInSeconds > 0)
            {
                NewLineAndTab(reportStringBuilder);
                reportStringBuilder.Append($"DURATION: {testCase.DurationInSeconds} seconds");
            }

            NewLineAndTab(reportStringBuilder);
            reportStringBuilder.Append($"START TIME: {testCase.StartDateTime}");

            NewLineAndTab(reportStringBuilder);
            reportStringBuilder.Append($"END TIME: {testCase.EndDateTime}");

            if (testCase.AllAdditionalData.HasAny())
            {
                NewLineAndTab(reportStringBuilder);
                reportStringBuilder.Append($"ADDITIONAL DATA: {testCase.AllAdditionalData.ConvertToString()}");
            }

            if (testCase.FailAdditionalData.HasAny())
            {
                NewLineAndTab(reportStringBuilder);
                reportStringBuilder.Append($"FAIL ADDITIONAL DATA: {testCase.FailAdditionalData.ConvertToString()}");
            }

            if (testCase.TestObjectInfo != null)
            {
                NewLineAndTab(reportStringBuilder);
                reportStringBuilder.Append($"TEST OBJECT INFO: {testCase.TestObjectInfo}");
            }

            if (testCase.Exception != null)
            {
                NewLineAndTab(reportStringBuilder);
                reportStringBuilder.Append($"EXCEPTION: {testCase.Exception}");
            }

            IList<TestStep> testSteps = testCase.TestSteps;

            if (testSteps.HasAny())
            {
                testSteps.ToList().ForEach(x =>
                {
                    NewLineAndTab(reportStringBuilder);
                    NewLineAndTab(reportStringBuilder);

                    reportStringBuilder.Append($"STEP: {x.Method.Name}");

                    NewLineAndTab(reportStringBuilder);
                    reportStringBuilder.Append($"RESULT: {x.Result}");

                    if (x.DurationInSeconds > 0)
                    {
                        NewLineAndTab(reportStringBuilder);
                        reportStringBuilder.Append($"DURATION: {x.DurationInSeconds} seconds");
                    }

                    NewLineAndTab(reportStringBuilder);
                    reportStringBuilder.Append($"START TIME: {x.StartDateTime}");

                    NewLineAndTab(reportStringBuilder);
                    reportStringBuilder.Append($"END TIME: {x.EndDateTime}");

                    if (x.AllAdditionalData.HasAny())
                    {
                        NewLineAndTab(reportStringBuilder);
                        reportStringBuilder.Append($"ADDITIONAL DATA: {x.AllAdditionalData.ConvertToString()}");
                    }

                    if (x.FailAdditionalData.HasAny())
                    {
                        NewLineAndTab(reportStringBuilder);
                        reportStringBuilder.Append($"FAIL ADDITIONAL DATA: {x.FailAdditionalData.ConvertToString()}");
                    }

                    if (x.TestObjectInfo != null)
                    {
                        NewLineAndTab(reportStringBuilder);
                        reportStringBuilder.Append($"TEST OBJECT INFO: {x.TestObjectInfo}");
                    }

                    if (x.Exception != null)
                    {
                        NewLineAndTab(reportStringBuilder);
                        reportStringBuilder.Append($"EXCEPTION: {x.Exception}");
                    }
                });
            }

            NewLineAndTab(reportStringBuilder);
            NewLineAndTab(reportStringBuilder);

            if (testCase.Result.Equals(TestResult.Passed))
            {
                logger.Information(reportStringBuilder.ToString());
            }
            else
            {
                logger.Error(reportStringBuilder.ToString());
            }
        }

        private void NewLineAndTab(StringBuilder reportStringBuilder)
        {
            reportStringBuilder.Append($"{Environment.NewLine}");
            reportStringBuilder.Append("\t");
        }
    }
}