using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog
{
    public struct Property
    {
        public string name;
        public string value;
    }

    class MQ
    {
        private string URI;
        private string TOPIC;
        private string USERNAME;
        private string PASSWORD;
        private IConnectionFactory factory;
        private IConnection connection;
        private ISession session;
        private IMessageProducer producer;

        public string uri
        {
            set { URI = value; }
            get { return URI; }
        }

        public string topic
        {
            set { TOPIC = value; }
            get { return TOPIC; }
        }

        public string username
        {
            set { USERNAME = value; }
        }

        public string password
        {
            set { PASSWORD = value; }
        }

        public MQ()
        {
            producer = null;
            factory = null;
            connection = null;
            session = null;
        }

        ~MQ()
        {
            if (producer != null)
            {
                producer.Dispose();
            }

            Close();
        }

        public void Start()
        {
            factory = new ConnectionFactory(URI);

            if (USERNAME != "")
            {
                connection = factory.CreateConnection(USERNAME, PASSWORD);
            }
            else
            {
                connection = factory.CreateConnection();
            }
            connection.ClientId = "haikan"; //持久订阅需要设置这个。

            connection.Start();
            session = connection.CreateSession();
        }

        public void Close()
        {
            if (session != null)
            {
                session.Close();
            }
            if (connection != null)
            {
                connection.Stop();
                connection.Close();
            }
        }

        public void CreateProducer(bool blnTopic, string strTopicName)
        {
            if (blnTopic)
            {
                producer = session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(strTopicName));
            }
            else
            {
                producer = session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(strTopicName));
            }
        }

        public void CreateProducer(bool blnTopic)
        {
            if (blnTopic)
            {
                producer = session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(TOPIC));
            }
            else
            {
                producer = session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(TOPIC));
            }
        }

        public IMessageConsumer CreateConsumer(bool blnTopic, string strTopicName)
        {
            if (blnTopic)
            {
                //return session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(strTopicName));
                return session.CreateDurableConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(strTopicName), "haikan", null, true);
            }
            else
            {
                return session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(strTopicName));
            }
        }

        public IMessageConsumer CreateConsumer(bool blnTopic, string strTopicName, string strSelector)
        {

            if (blnTopic)
            {
                return session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(strTopicName), strSelector, false);
            }
            else
            {
                return session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(strTopicName), strSelector, false);
            }
        }

        public void SendMQMessage(string strText)
        {
            ITextMessage msg = producer.CreateTextMessage();
            msg.Text = strText;
            producer.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent, Apache.NMS.MsgPriority.Normal, TimeSpan.MinValue);
        }

        public void SendMQMessage(string strText, List<Property> lstProperty)
        {
            try
            {
                ITextMessage msg = producer.CreateTextMessage();
                msg.Text = strText;

                foreach (Property prop in lstProperty)
                {
                    msg.Properties.SetString(prop.name, prop.value);
                }
                producer.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent, Apache.NMS.MsgPriority.Normal, TimeSpan.MinValue);
            }
            catch (System.Exception ex)
            {
                //GlobalFunction.MsgBoxException(ex.Message, "SendMQMessage");
            }
        }
    }
}
