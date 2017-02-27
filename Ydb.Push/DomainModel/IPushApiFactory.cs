using System;

using Ydb.Common;

namespace Ydb.Push.DomainModel
{
    public interface IPushApiFactory
    {
        IPushApi Create(PushMessage message, PushTargetClient pushType, string type, bool isDebug);
    }
}