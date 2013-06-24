About
-----
GigaNoodle is a Window's service that can work on items in a Queue asynchronously. 

Usage
-----
Install the service, then configure queues. Queues are configured in the App.config file. Configuration syntax is subjet to change. Currently a sample configuration looks like:

    <configSections>
      <section
        name="gigaNoodleConfig"
        type="GigaNoodle.WindowsService.Config.GigaNoodleConfigHandler, GigaNoodle.WindowsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        allowLocation="true"
        allowDefinition="Everywhere"
        />
    </configSections>
    <gigaNoodleConfig>
      <queues>
        <queue name="ConsoleTestQueue" type="GigaNoodle.RabbitMQueue.RabbitMQueue, GigaNoodle.RabbitMQueue, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" />
      </queues>
      <queueItemTypes>
        <queueItemType name="console1" knownType="ConsoleApplication1.Program+MyQueueItem, ConsoleApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"></queueItemType>
      </queueItemTypes>
    </gigaNoodleConfig>

To use the queue in your project, add GigaNoodle.Library as a reference in your project. Then define an object that inherits from QueueItem. That QueueItem can be pushed onto a specific Queue like:

    var queue = (IQueue)new RabbitMQueue("ConsoleTestQueue", 1, typeof(MyQueueItem));
    var queueItem = new MyQueueItem();
    queue.Push(queueItem);

Notes
-----
GigaNoodle is in pre-beta stage. Everything is subject to change. Taking any and all input currently. 
