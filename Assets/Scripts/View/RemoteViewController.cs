using SmartGlass;
using SmartGlass.Messaging.Session.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RemoteViewController : ViewController
{
    internal override void OnInit()
    {
        var tBtns = transform.GetComponentsInChildren<Button>(true);
        foreach (var tBtn in tBtns)
        {
            var tBtnType = GameButtonState.Clear;
            var tName = tBtn.name;
            Action tAction = null;
            if (Enum.TryParse(tName, false, out tBtnType))
            {
                tAction = () =>
                 {
                     OnClick_Btn(tBtnType);
                 };
            }
            else
            {
                switch (tName)
                {
                    case "Edge":
                        tAction = () =>
                        {
                            UIManager.OpenView<EdgeViewController>();
                        };
                        break;
                    case "Back":
                        tAction = () =>
                        {
                            OnClick_Btn(GameButtonState.B);
                        };
                        break;
                    case "OK":
                        tAction = () =>
                        {
                            OnClick_Btn(GameButtonState.A);
                        };
                        break;
                    case "Home":
                        tAction = () =>
                        {
                            OnClick_Btn(GameButtonState.Nexus);
                        };
                        break;
                    //加音量
                    case "AddVolume":
                        tAction = () =>
                        {
                            TipsManager.ShowTips("未实现");

                            //SmartGlassManager.instance.SendMediaState(new MediaCommandMessage()
                            //{
                            //    //State = new MediaState()
                            //    //{
                            //    //    SoundLevel = MediaSoundLevel.Full
                            //    //}
                            //});
                        };
                        break;
                    //减音量
                    case "ReduceVolume":
                        tAction = () =>
                        {
                            TipsManager.ShowTips("未实现");

                            //SmartGlassManager.instance.SendMediaState(new MediaStateMessage()
                            //{
                            //    State = new MediaState()
                            //    {
                            //        SoundLevel = MediaSoundLevel.Low
                            //    }
                            //});
                        };
                        break;
                    case "PgUp":
                        tAction = () =>
                        {
                            SendMediaState(MediaControlCommands.ChannelUp);
                        };
                        break;
                    case "PgDown":
                        tAction = () =>
                        {
                            SendMediaState(MediaControlCommands.ChannelDown);
                        };
                        break;
                    //未知功能
                    case "Unknown":
                        tAction = () =>
                        {
                            TipsManager.ShowTips("未实现");
                        };
                        break;
                    //静音
                    case "Mute":
                        tAction = () =>
                        {
                            TipsManager.ShowTips("未实现");
                            //SmartGlassManager.instance.SendMediaState(new MediaStateMessage()
                            //{
                            //    State = new MediaState()
                            //    {
                            //        SoundLevel = MediaSoundLevel.Muted
                            //    }
                            //});
                        };
                        break;
                    //快退
                    case "Backward":
                        tAction = () =>
                        {
                            SendMediaState(MediaControlCommands.Back);
                        };
                        break;
                    //快进
                    case "Forward":
                        tAction = () =>
                        {
                            SendMediaState(MediaControlCommands.FastForward);
                        };
                        break;
                    //播放或者暂停
                    case "PlayOrPause":
                        tAction = () =>
                        {
                            SendMediaState(MediaControlCommands.PlayPauseToggle);
                        };
                        break;
                    //上一首
                    case "Previous":
                        tAction = () =>
                        {
                            SendMediaState(MediaControlCommands.PreviousTrack);
                        };
                        break;
                    //下一首
                    case "Next":
                        tAction = () =>
                        {
                            SendMediaState(MediaControlCommands.NextTrack);
                        };
                        break;
                    //停止播放
                    case "Stop":
                        tAction = () =>
                        {
                            SendMediaState(MediaControlCommands.Stop);
                        };
                        break;
                    default:
                        Debug.LogErrorFormat("GameButtonState转换失败：{0}", tBtn.name);
                        break;
                }
            }

            if (tAction != null)
            {
                tBtn.onClick.AddListener(new UnityAction(tAction));
            }
        }
    }

    void OnClick_Btn(GameButtonState pBtnType)
    {
        Debug.LogFormat($"Click {pBtnType.ToString()}");

        var tGamepadState = GamepadButtons.Clear;
        if (Utils.TryEnumParse(pBtnType, out tGamepadState))
        {
            SmartGlassManager.instance.SendGamepadState(new GamepadState()
            {
                Buttons = tGamepadState
            });
        }
        else
        {
            throw new Exception(string.Format($"未实现：{pBtnType.ToString()}"));
        }
    }

    void SendMediaState(MediaControlCommands pState)
    {
        SmartGlassManager.instance.SendMediaState(new MediaCommandMessage()
        {
            State = new MediaCommandState()
            {
                Command = pState
            }
        });
    }
}
