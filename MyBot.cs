using Discord;
using Discord.Commands;
using Discord.Modules;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BOT1
{
    class MyBot
    {
        DiscordClient discord;


        string[] randomPof;
        string[] randomPfc;

        Random rand;



        public MyBot()

        {
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;

            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '.';
                x.AllowMentionPrefix = true;
            });

            rand = new Random();


            randomPof = new string[]
            {
                "PILE",
                "FACE"
            };

            randomPfc = new string[]
            {
                "PIERRE",
                "FEUILLE",
                "CISEAUX"
            };

            var commands = discord.GetService<CommandService>();

            //------------------------------------------BIERE------------------------------------------------------

            commands.CreateCommand("bière")
                 .Parameter("@Chibi", ParameterType.Unparsed)
                 .Do(async (e) =>
                 {
                     await Task.Delay(500);
                     await e.Message.Delete();
                     await e.Channel.SendMessage($"_ {e.User.Name} trinque avec {e.GetArg("@Chibi")} :beer: _");
                 });

            //-------------------------------------------BONJOUR----------------------------------------------------

            commands.CreateCommand("bonjour")
               .Alias(new string[] { "bj", "hi" })
               .Parameter("@Chibi", ParameterType.Unparsed)
               .Do(async (e) =>
               {
                   await Task.Delay(500);
                   await e.Message.Delete();
                   await e.Channel.SendMessage($"_ {e.User.Name} salue {e.GetArg("@Chibi")} _ :slight_smile: ");
               });
            //--------------------------------------------CALINS----------------------------------------------------

            commands.CreateCommand("câlin")
                .Alias(new string[] { "cl" })
                .Parameter("@Chibi", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    await Task.Delay(500);
                    await e.Message.Delete();
                    await e.Channel.SendMessage($"_ {e.User.Name} fait un câlin à {e.GetArg("@Chibi")} ♥ _");
                });
            //--------------------------------------------BISOUS----------------------------------------------------

            commands.CreateCommand("bisous")
                .Alias(new string[] { "bs" })
                .Parameter("@Chibi", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    await Task.Delay(500);
                    await e.Message.Delete();
                    await e.Channel.SendMessage($"_ {e.User.Name} embrasse {e.GetArg("@Chibi")} ♥ _");
                });

            //--------------------------------------------BONNE NUIT----------------------------------------------------

            commands.CreateCommand("nuit")
                .Alias(new string[] { "bn" })
                .Parameter("@Chibi", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    await Task.Delay(500);
                    await e.Message.Delete();
                    await e.Channel.SendMessage($"_ {e.User.Name} te souhaite une bonne nuit {e.GetArg("@Chibi")} _");
                });

            //----------------------------------------------SOFT-------------------------------------------------------

            commands.CreateCommand("soft")
                .Alias(new string[] { "thé" })
                .Parameter("@Chibi", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    await Task.Delay(500);
                    await e.Message.Delete();
                    await e.Channel.SendMessage($"_  {e.User.Name} sert un thé à {e.GetArg("@Chibi")} _");
                });

            //---------------------------------------------HELP-----------------------------------------------------

            /* commands.CreateCommand("help")
                 .Alias(new string[] { "aide", "infos" })
                 .Do(async (e) =>
                 {
                     await Task.Delay(2000);
                     await e.Message.Delete();
                     await e.Channel.SendMessage("**_" + "Commandes :" + "_**");
                     await e.Channel.SendMessage("*" + ".bonjour .bj ou .hi suivi du nom de la personne visée" + "*");
                     await e.Channel.SendMessage("*" + ".câlin ou .cl suivi du nom de la personne visée" + "*");
                     await e.Channel.SendMessage("*" + ".bière suivi du nom de la personne visée" + "*");
                     await e.Channel.SendMessage("*" + ".bisous ou .bs suivi du nom de la personne visée" + "*");
                     await e.Channel.SendMessage("*" + ".nuit ou .bn suivi du nom de la personne visée" + "*");
                     await e.Channel.SendMessage("**_" + "Jeux :" + "_**");
                     await e.Channel.SendMessage("*" + ".pfc : Pierre, Feuille, Ciseaux" + "*");
                     await e.Channel.SendMessage("*" + ".pof : Pile ou Face" + "*");
                     await e.Channel.SendMessage("*" + ".d6 , .d20 , .d100 : Dés (6, 20, 100)" + "*");
                     await e.Channel.SendMessage("**_" + "Les commandes ne fonctionnent que si le Bot est connecté." + "_**");
                 }); */

            commands.CreateCommand("help")
                .Alias(new string[] { "aide", "infos" })
                .Do(async (e) =>
                    {
                        await Task.Delay(500);
                        await e.Message.Delete();
                        await e.Channel.SendMessage(e.User.Mention + "**_ " + "Commandes : " + "_**" + "_(suivi d'un espace et du nom de la personne concernée)_" + "\r\n" +
                                                                     "```" + ".bonjour, .câlin, .bière, .bisous, .nuit," + "```");

                        await e.Channel.SendMessage(e.User.Mention + "**_ " + "Jeux : " + "_**" + "_(pile ou face, PFC, dés)_" + "\r\n" +
                                                                     "```" + " .pfc, .pof, .d6, .d20, .d100" + "```");
                    });

            //------------------------------------------------------------------------------------------------------




            //--------------------------------------------CONNECT STATUS--------------------------------------------

            discord.UserJoined += async (s, e) =>
            {
                await e.Server.DefaultChannel.SendMessage(e.User.Mention + " à rejoint le serveur ! :grinning:");
            };

            discord.UserLeft += async (s, e) =>
            {
                await e.Server.DefaultChannel.SendMessage(e.User.Mention + " à été expulsé du serveur.");
            };

            discord.UserUpdated += async (s, e) =>
            {

                if (e.Before.Nickname == null)
                    if (e.After.Nickname != null)
                        //if (e.Before.Name != e.After.Nickname)
                        await e.Server.DefaultChannel.SendMessage(e.Before.Name + " à modifié son pseudo : " + e.After.NicknameMention);
            };

            discord.UserUpdated += async (s, e) =>
            {

                if (e.Before.Nickname != null)
                    if (e.Before.Nickname != e.After.Nickname)
                        await e.Server.DefaultChannel.SendMessage(e.Before.Name + " à modifié son pseudo : " + e.After.NicknameMention);
            };


            /* discord.UserUpdated += async (s, e) =>
            {
                if (discord.State != ConnectionState.Connected) return;
                await e.Server.DefaultChannel.SendMessage(e.After.Name + " a modifié son statut");

                
            }; */
           



            //----------------------------------------------BOT-----------------------------------------------------

            commands.CreateCommand("bot")
                .Do(async (e) =>
                {
                    await Task.Delay(500);
                    await e.Message.Delete();
                    await e.Channel.SendMessage("(╯°□°）╯︵ ┻━┻");
                });

            commands.CreateCommand("botdel")
                .Do(async (e) =>
                {
                    Message[] messageToDelete;
                    messageToDelete = await e.Channel.DownloadMessages(5);
                    await e.Channel.DeleteMessages(messageToDelete);
                });

            commands.CreateCommand("left")
                .Do(async (e) =>
               {
                   await e.Message.Delete();
                   await e.Channel.SendMessage("_Bye_" + " :wave:");
                   await Task.Delay(500);
                   await discord.Disconnect();
               });

            commands.CreateCommand("playin")
                .Parameter("jeu", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    await e.Message.Delete();
                    var Z = e.GetArg("jeu");
                    discord.SetGame(Z);
                });

            
            //------------------------------------------------QUERY---------------------------------------------------

            commands.CreateCommand("say")
               .Parameter("query", ParameterType.Unparsed)
               .Do(async (e) =>
               {
                   await e.Message.Delete();
                   await e.Channel.SendMessage(e.GetArg("query"));
               });

            //------------------------------------------------JEUX---------------------------------------------------

            commands.CreateCommand("pof")
        .Do(async (e) =>
        {
            await Task.Delay(500);
            await e.Message.Delete();
            int RandomTextIndex = rand.Next(randomPof.Length);
            string textToPost = randomPof[RandomTextIndex];
            await e.Channel.SendMessage(e.User.Mention + " fait " + "**_" + textToPost + "_**");
        });

            commands.CreateCommand("pfc")
                .Do(async (e) =>
                {
                    await Task.Delay(500);
                    await e.Message.Delete();
                    int RandomTextIndex = rand.Next(randomPfc.Length);
                    string pfcToPost = randomPfc[RandomTextIndex];
                    await e.Channel.SendMessage(e.User.Mention + " fait " + "**_" + pfcToPost + "_**");
                });


            //------------------------------------------------------------------------------------------------------------------------------------

            commands.CreateCommand("denis")
                  .Do(async (e) =>
                  {
                      await Task.Delay(500);
                      await e.Message.Delete();
                      await e.Channel.SendMessage("_SuperDenis s'installe confortablement dans son fauteuil et écoute sa chanson préferée :_" + "\r\n" + "https://www.youtube.com/watch?v=ahGxiSV_LH0");
                  });

            //------------DéS------------------------

            commands.CreateCommand("d6")
               .Do(async (e) =>
               {
                   await Task.Delay(500);
                   await e.Message.Delete();
                   int Randomd6 = rand.Next(1, 6);
                   await e.Channel.SendMessage(e.User.Mention + "_" + " lance un dé " + "**" + "6" + "**" + " et fait un " + "_" + "**_" + Randomd6 + "_**");
               });

            commands.CreateCommand("d20")
                .Do(async (e) =>
                {
                    await Task.Delay(500);
                    await e.Message.Delete();
                    int Randomd20 = rand.Next(1, 20);
                    await e.Channel.SendMessage(e.User.Mention + "_" + " lance un dé " + "**" + " 20 " + "**" + " et fait un " + "_" + "**_" + Randomd20 + "_**");
                });

            commands.CreateCommand("d100")
               .Do(async (e) =>
               {
                   await Task.Delay(500);
                   await e.Message.Delete();
                   int Randomd100 = rand.Next(1, 100);
                   await e.Channel.SendMessage(e.User.Mention + "_" + " lance un dé " + "**" + " 100 " + "**" + " et fait un " + "_" + "**_" + Randomd100 + "_**");
               });

            commands.CreateCommand("lien")
                .Do(async (e) =>
                {
                    await Task.Delay(500);
                    await e.Message.Delete();
                    await e.Channel.SendMessage("**_" + "Lien d'invitation illimité :" + "_**");
                    await e.Channel.SendMessage("```" + "https://discord.gg/QTyFGy2" + "```");
                });

            //-----------------------------------------VOICE CHANNEL STATUT----------------------------------------

            discord.UserUpdated += (s, e) =>
            {
                if (e.After.VoiceChannel == null) return;
                if (e.Before.VoiceChannel == e.After.VoiceChannel) return;

                /* var msg = await e.Before.SendMessage($"Vous êtes maintenant dans le salon vocal : **_ {e.After.VoiceChannel} _** !");
                await Task.Delay(30000);
                await msg.Delete(); */

                Console.WriteLine(e.After.Name + " a changé de salon vocal : " + e.After.VoiceChannel);
            };

            //await e.Server.DefaultChannel.SendMessage($"{e.After.Name} a rejoint le salon : {e.After.VoiceChannel}");   


            //-----------------------------------------CONNEXION BOT----------------------------------------------

            

            discord.ExecuteAndWait (async () =>
                {
                    while (true)
                    {

                        
                        try
                        {
                            discord.ServerAvailable += async (s, e) => await e.Server.DefaultChannel.SendMessage(":wave:");                            
                            await discord.Connect("MjIzODcxOTAyMDQzNjAyOTU0.CtJYsA.gS0O2C-Leq34JuYDfNxafyVJHiA", TokenType.Bot);
                            break;
                        }
                        catch
                        {
                            await Task.Delay(3000);

                        }
                    }
                });
        }


        private void Log(object sender, LogMessageEventArgs e)
        {

            
            Console.WriteLine(e.Message);
            discord.SetGame("♥♥♥");
           
            
            
        }
    }



}

