using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Data;

namespace Alert.Domain.Transitions
{
    // I want to feed this thing a stream of entities and have it change state accordingly

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TStateEnum"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public class StateMachine<TStateEnum, TData> : Entity 
        where TStateEnum : struct
        where TData : Entity
    {
        public Dictionary<TStateEnum, Func<Context, Transform>> Commands { get; protected set; }

        public abstract class Transform : Entity
        {
            public abstract bool CanTransform(IContext context);
            public abstract State<TStateEnum> Transform(Context context);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TStateEnum">Your struct</typeparam>
    public class State<TStateEnum> : Entity 
        where TStateEnum : struct
        where TData : Entity
    {
        public State()
        {
            Commands = new Dictionary<TStateEnum,Func<Context>>();
        }
        public Dictionary<TStateEnum, Func<Context>> Commands { get; protected set; }

        public abstract class Transform : Entity
        {
            public abstract void Transform(Context context);
        }
    }

    enum FanCommand
    {
        TurnOn,
        TurnOff,
        PlugIn,
        Unplug
    }

    enum FanPlugStatus
    {
        PluggedIn,
        Unplugged
    }

    enum FanPower
    {
        On,
        Off
    }

    public class Fan
    {
        public void PowerUp();
        public void PowerDown();
    }

    public class TurnOff : State<FanPower>.Transform
    {
        public override void Transform(Context context)
        {
            context.Data().PowerDown();
        }
    }


    public class StateInitializer
    {

        public State<FanPower> FanMachine()
        {
            var machine = new State<FanPower>();
            
            machine.Commands.Add(FanCommand.TurnOff, context => context.Data().PowerDown());
        }
    }

    // Context.Commands[FanState.On]();
    public abstract class Context<TCommandEnum, TStateEnum> : Entity
    {
        public State<TStateEnum> State { get; protected set; }

        public Dictionary<TCommandEnum, Func<State<TStateEnum>.Transform>> Commands { get; protected set; }
    }

}
