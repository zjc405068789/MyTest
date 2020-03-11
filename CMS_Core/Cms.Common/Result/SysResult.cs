using Cms.Common.Extensions;

namespace Cms.Common.Result
{
    public class SysResult<T>
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public SysStatusCode StatusCode { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public T Result { get; set; }

        string _errorDesc;
        /// <summary>
        /// 附加消息
        /// </summary>
        public string ErrorDesc
        {
            get
            {

                if (_errorDesc == null && StatusCode != SysStatusCode.sys_success)
                {
                    return StatusCode.GetDescription();
                }
                return _errorDesc;

            }
            set { _errorDesc = value; }
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success
        {
            get { return StatusCode == SysStatusCode.sys_success; }
        }
    }
}
