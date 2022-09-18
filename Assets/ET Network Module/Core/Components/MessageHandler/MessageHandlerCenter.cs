﻿using ET;
using System;
public static class MessageHandlerCenter
{
    public static void ListenSignal<Message>(Action<Session, Message> task) where Message : class, IMessage
    {
        var type = typeof(Message);
        if (!typeof(IResponse).IsAssignableFrom(type) && !typeof(IRequest).IsAssignableFrom(type))
        {
            var instance = ET.MessageDispatcher.GetHandler<Message>(type);
            instance?.Register(task);
        }
        else
        {
            UnityEngine.Debug.LogError($"{nameof(MessageHandlerCenter)}: 不支持消息 {type.Name} ， 请尝试监听无返回值的网络消息！");
        }
    }

    public static void RemoveSignal<Message>(Action<Session, Message> task) where Message : class, IMessage
    {
        var instance = ET.MessageDispatcher.GetHandler<Message>(typeof(Message));
        instance?.UnRegister(task);
    }
}