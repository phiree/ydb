namespace Dianzhu.ApplicationService.App
{
    public interface IAppService
    {
        /// <summary>
        /// 注册设备,userID 为空，表示匿名注册
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appobj"></param>
        /// <returns></returns>
        object PostDeviceBind(string id, appObj appobj);

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        object DeleteDeviceBind(string id);

        /// <summary>
        /// 更新设备推送计数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pushCount"></param>
        /// <returns></returns>
        object PatchDeviceBind(string id, string pushCount);
    }
}