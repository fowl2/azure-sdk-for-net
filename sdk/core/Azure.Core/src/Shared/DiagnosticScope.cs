// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;

#if NETFRAMEWORK
namespace System.Diagnostics
{
    internal sealed class ActivitySource { public ActivitySource(string n) {} }
    internal sealed class ActivityContext { }
    internal sealed class ActivityLink { }
    internal sealed class ActivityTagsCollection { }
    internal enum ActivityKind { Internal, Client }
}
#endif

namespace Azure.Core.Pipeline
{
    internal readonly struct DiagnosticScope : IDisposable
    {
        internal const string OpenTelemetrySchemaAttribute = "az.schema_url";

        // we follow OpenTelemtery Semantic Conventions 1.23.0
        // https://github.com/open-telemetry/semantic-conventions/blob/v1.23.0
        internal const string OpenTelemetrySchemaVersion = "https://opentelemetry.io/schemas/1.23.0";


        [RequiresUnreferencedCode("The diagnosticSourceArgs are used in a call to DiagnosticSource.Write, all necessary properties need to be preserved on the type being passed in using DynamicDependency attributes.")]
        internal DiagnosticScope(string scopeName, DiagnosticListener source, object? diagnosticSourceArgs, ActivitySource? activitySource, System.Diagnostics.ActivityKind kind, bool suppressNestedClientActivities)
        {
        }

        public bool IsEnabled { get; }

        public void AddAttribute(string name, string? value)
        {
        }

        public void AddIntegerAttribute(string name, int value)
        {
        }

        public void AddLongAttribute(string name, long value)
        {
        }

        public void AddAttribute<T>(string name, T value, Func<T, string> format)
        {
        }

        /// <summary>
        /// Adds a link to the scope. This must be called before <see cref="Start"/> has been called for the DiagnosticScope.
        /// </summary>
        /// <param name="traceparent">The traceparent for the link.</param>
        /// <param name="tracestate">The tracestate for the link.</param>
        /// <param name="attributes">Optional attributes to associate with the link.</param>
        public void AddLink(string traceparent, string? tracestate, IDictionary<string, object?>? attributes = null)
        {
        }

        public void Start()
        {
        }

        public void SetDisplayName(string displayName)
        {
        }

        public void SetStartTime(DateTime dateTime)
        {
        }

        /// <summary>
        /// Sets the trace context for the current scope.
        /// </summary>
        /// <param name="traceparent">The trace parent to set for the current scope.</param>
        /// <param name="tracestate">The trace state to set for the current scope.</param>
        public void SetTraceContext(string traceparent, string? tracestate = default)
        {
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// Marks the scope as failed.
        /// </summary>
        /// <param name="exception">The exception to associate with the failed scope.</param>
        [UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "The Exception being passed into this method has public properties preserved on the inner method MarkFailed." +
            "The public property System.Exception.TargetSite.get is not compatible with trimming and produces a warning when preserving all public properties. Since we do not use this property, and" +
            "neither does Application Insights, we can suppress the warning coming from the inner method.")]
        public void Failed(Exception exception)
        {
        }

        /// <summary>
        /// Marks the scope as failed with low-cardinality error.type attribute.
        /// </summary>
        /// <param name="errorCode">Error code to associate with the failed scope.</param>
        [UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "The public property System.Exception.TargetSite.get is not compatible with trimming and produces a warning when " +
            "preserving all public properties. Since we do not use this property, and neither does Application Insights, we can suppress the warning coming from the inner method.")]
        public void Failed(string errorCode)
        {
        }
    }

#pragma warning disable SA1507 // File can not contain multiple types
    /// <summary>
    /// Until Activity Source is no longer considered experimental.
    /// </summary>
    internal static class ActivityExtensions
    {
        static ActivityExtensions()
        {
            ResetFeatureSwitch();
        }

        public static bool SupportsActivitySource { get; private set; }

        public static void ResetFeatureSwitch()
        {
            SupportsActivitySource = AppContextSwitchHelper.GetConfigValue(
                "Azure.Experimental.EnableActivitySource",
                "AZURE_EXPERIMENTAL_ENABLE_ACTIVITY_SOURCE");
        }
    }
}
