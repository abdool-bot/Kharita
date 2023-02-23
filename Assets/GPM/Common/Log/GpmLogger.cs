using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Gpm.Common.Log
{
    public static class GpmLogger
    {
        public enum LogLevelType
        {
            NONE = 0,
            ERROR,
            WARN,
            DEBUG,
            ALL,
        }

        public static bool LogEnabled { get; set; } = true;

        public static LogLevelType LogLevel { get; set; } = LogLevelType.WARN;

        /// <summary>
        /// Generates a log message.
        /// </summary>
        /// <param name="message">required</param>
        /// <param name="serviceName">required</param>
        /// <param name="classType">required</param>
        /// <param name="methodName">optional</param>
        /// <returns>[GPM][ServiceName][ClassName::MethodName] message</returns>
        private static string MakeLog(object message, string serviceName, Type classType, string methodName)
        {
            StringBuilder log = new StringBuilder("[GPM]");
            log.AppendFormat("[{0}]", serviceName);
            log.AppendFormat("[{0}", classType.Name);
            log.AppendFormat("::{0}]", methodName);
            log.AppendFormat(" {0}", message);

            return log.ToString();
        }

        /// <summary>
        /// 1. 디버그 로그
        /// 2. 함수 흐름에 관한 로그
        /// 3. 데이터 로그
        /// </summary>
        public static void Debug(object message, string serviceName, Type classType,
#if CSHARP_7_3_OR_NEWER
            [CallerMemberName]
#endif
            string methodName = "")
        {
            if (GpmCommon.DebugLogEnabled == false)
            {
                if (LogEnabled == false)
                {
                    return;
                }

                if (LogLevel < LogLevelType.DEBUG)
                {
                    return;
                }
            }
            
            UnityEngine.Debug.Log(MakeLog(message, serviceName, classType, methodName));
        }

        /// <summary>
        /// 애플리케이션 흐름에는 영향이 없으나 제한되거나 권장하지 않는 흐름에 대한 로그
        /// </summary>
        public static void Warn(object message, string serviceName, Type classType,
#if CSHARP_7_3_OR_NEWER
            [CallerMemberName] 
#endif
            string methodName = "")
        {
            if (GpmCommon.DebugLogEnabled == false)
            {
                if (LogEnabled == false)
                {
                    return;
                }

                if (LogLevel < LogLevelType.WARN)
                {
                    return;
                }
            }

            UnityEngine.Debug.LogWarning(MakeLog(message, serviceName, classType, methodName));
        }

        /// <summary>
        /// 애플리케이션 흐름에 치명적인 영향이 있는 오류
        /// </summary>
        public static void Error(object message, string serviceName, Type classType,
#if CSHARP_7_3_OR_NEWER
            [CallerMemberName] 
#endif
            string methodName = "")
        {
            if (GpmCommon.DebugLogEnabled == false)
            {
                if (LogEnabled == false)
                {
                    return;
                }

                if (LogLevel < LogLevelType.ERROR)
                {
                    return;
                }
            }

            UnityEngine.Debug.LogError(MakeLog(message, serviceName, classType, methodName));
        }
    }
}
