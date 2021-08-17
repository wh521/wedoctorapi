using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDectorApi.Job
{
    public class SimpleTriggerRunner
    {
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="withIntervalInSeconds">间隔时间(秒)</param>
        /// <returns></returns>
        public virtual async Task Run_SendOrderList(int withIntervalInSeconds)
        {
            //创建调度工厂
            StdSchedulerFactory factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();
            //校验作业是否存在
            if (!scheduler.CheckExists(new JobKey("SendOrderListJob", "SendOrderListGroup")).Result)
            {
                await scheduler.Start();
                //定义作业并绑定作业业务类
                IJobDetail job = JobBuilder.Create<SendOrderListJob>()
                    .WithIdentity("SendOrderListJob", "SendOrderListGroup")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("SendOrderListTrigger", "SendOrderListGroup")
                    .StartNow()
                    .WithSimpleSchedule((x) => x
                        .WithIntervalInSeconds(withIntervalInSeconds)
                        .RepeatForever()
                    )
                .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
        }

        /// <summary>
        /// 移除查询订单作业
        /// </summary>
        /// <returns></returns>
        public virtual async Task Delete_SendOrderList()
        {
            await DeleteJob("SendOrderListJob", "SendOrderListGroup");
        }

        /// <summary>
        /// 标识订单异常
        /// </summary>
        /// <param name="withIntervalInSeconds">间隔时间(秒)</param>
        /// <returns></returns>
        public virtual async Task Run_SendOrderRefuse(int withIntervalInSeconds)
        {
            //创建调度工厂
            StdSchedulerFactory factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();
            //校验作业是否存在
            if (!scheduler.CheckExists(new JobKey("SendOrderRefuseJob", "SendOrderRefuseGroup")).Result)
            {
                await scheduler.Start();

                //定义作业并绑定作业业务类
                IJobDetail job = JobBuilder.Create<SendOrderRefuseJob>()
                    .WithIdentity("SendOrderRefuseJob", "SendOrderRefuseGroup")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("SendOrderRefuseTrigger", "SendOrderRefuseGroup")
                    .StartNow()
                    .WithSimpleSchedule((x) => x
                        .WithIntervalInSeconds(withIntervalInSeconds)
                        .RepeatForever()
                    )
                .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
        }

        /// <summary>
        /// 移除标识订单异常同步作业
        /// </summary>
        /// <returns></returns>
        public virtual async Task Delete_SendOrderRefuse()
        {
            await DeleteJob("SendOrderRefuseJob", "SendOrderRefuseGroup");
        }

        /// <summary>
        /// 订单发货
        /// </summary>
        /// <param name="withIntervalInSeconds">间隔时间(秒)</param>
        /// <returns></returns>
        public virtual async Task Run_SendOrderDelivery(int withIntervalInSeconds)
        {
            //创建调度工厂
            StdSchedulerFactory factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();
            //校验作业是否存在
            if (!scheduler.CheckExists(new JobKey("SendOrderDeliveryJob", "SendOrderDeliveryGroup")).Result)
            {
                await scheduler.Start();

                //定义作业并绑定作业业务类
                IJobDetail job = JobBuilder.Create<SendOrderDeliveryJob>()
                    .WithIdentity("SendOrderDeliveryJob", "SendOrderDeliveryGroup")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("SendOrderDeliveryTrigger", "SendOrderDeliveryGroup")
                    .StartNow()
                    .WithSimpleSchedule((x) => x
                        .WithIntervalInSeconds(withIntervalInSeconds)
                        .RepeatForever()
                    )
                .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
        }

        /// <summary>
        /// 移除订单发货同步作业
        /// </summary>
        /// <returns></returns>
        public virtual async Task Delete_SendOrderDelivery()
        {
            await DeleteJob("SendOrderDeliveryJob", "SendOrderDeliveryGroup");
        }

        /// <summary>
        /// 库存更新
        /// </summary>
        /// <param name="withIntervalInSeconds">间隔时间(秒)</param>
        /// <returns></returns>
        public virtual async Task Run_UpdateStock(int withIntervalInSeconds)
        {
            //创建调度工厂
            StdSchedulerFactory factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();
            //校验作业是否存在
            if (!scheduler.CheckExists(new JobKey("UpdateStockJob", "UpdateStockGroup")).Result)
            {
                await scheduler.Start();

                //定义作业并绑定作业业务类
                IJobDetail job = JobBuilder.Create<UpdateStockJob>()
                    .WithIdentity("UpdateStockJob", "UpdateStockGroup")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("UpdateStockTrigger", "UpdateStockGroup")
                    .StartNow()
                    .WithSimpleSchedule((x) => x
                        .WithIntervalInSeconds(withIntervalInSeconds)
                        .RepeatForever()
                    )
                .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
        }

        /// <summary>
        /// 移除库存同步更新作业
        /// </summary>
        /// <returns></returns>
        public virtual async Task Delete_UpdateStock()
        {
            await DeleteJob("UpdateStockJob", "UpdateStockGroup");
        }

        /// <summary>
        /// 根据Job名称、组删除作业
        /// </summary>
        /// <param name="name">作业名称</param>
        /// <param name="group">作业组</param>
        async Task DeleteJob(string name,string group)
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();

            JobKey jobKey = new JobKey(name, group);
            await scheduler.DeleteJob(jobKey);
            //await Task.Delay(-1);
            await scheduler.Shutdown();
        }































        public virtual async Task Run_TestJob()
        {
            //创建调度工厂
            StdSchedulerFactory factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();
            await scheduler.Start();

            //定义作业并绑定作业业务类
            IJobDetail job = JobBuilder.Create<TestJob>()
                .WithIdentity("testjob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("testTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule((x) => x
                    .WithIntervalInSeconds(1)
                    .RepeatForever()
                )
            .Build();

            await scheduler.ScheduleJob(job, trigger);
        }


        public virtual async Task Run_DemoJob()
        {
            //创建调度工厂
            StdSchedulerFactory factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();
            await scheduler.Start();

            //定义作业并绑定作业业务类
            IJobDetail job = JobBuilder.Create<DemoJob>()
                .WithIdentity("demojob", "groupDemo")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("demoTrigger", "groupDemo")
                .StartNow()
                .WithSimpleSchedule((x) => x
                    .WithIntervalInSeconds(1)
                    .RepeatForever()
                )
            .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
