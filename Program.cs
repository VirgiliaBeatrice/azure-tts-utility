using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.Extensions.CommandLineUtils;
using System.Text.Json;

public class Program {
    public class Identity {
        public string Key { get; set; } = "";
        public string Location { get; set; } = "";
    }

    static public void Main(string[] args) {
        var app = new CommandLineApplication();

        app.Name = "AzureTTS";
        app.Description = "A console utility for Azure Text-To-Speech service.";

        app.HelpOption("-?|-h|--help");

        var audioOption = app.Option("-a|--audio",
            "audio file",
            CommandOptionType.SingleValue);
        var txtOption = app.Option("-t|--txt",
            "txt file",
            CommandOptionType.SingleValue);
        var identityOption = app.Option("-i",
            "identity file(JSON)",
            CommandOptionType.SingleValue);

        var options = new List<CommandOption>() {
            audioOption, txtOption, identityOption
        };

        app.OnExecute(async () => {
            if (options.Any(e => e.HasValue() == false)) {
                app.ShowHint();

                return -1;
            } else {
                var identityPath = Path.Combine(Environment.CurrentDirectory, identityOption.Value());
                var audioPath = Path.Combine(Environment.CurrentDirectory, audioOption.Value());
                var textPath = Path.Combine(Environment.CurrentDirectory, txtOption.Value());
                var jsonString = File.ReadAllText(identityPath);
                var identity = JsonSerializer.Deserialize<Identity>(jsonString);
                var content = File.ReadAllText(textPath);

                if (identity != null)
                    await Action(content, audioPath, identity);

                return 0;
            }
        });

        app.Execute(args);
    }

    static async Task Action(string content, string audioPath, Identity identity) {
        try {
            await SynthesizeAudioAsync(content, audioPath, identity);

        }
        catch (Exception ex) {
            Console.WriteLine(ex.ToString());
        }
    }

    static async Task SynthesizeAudioAsync(string text, string audio, Identity identity) {
        var config = SpeechConfig.FromSubscription(identity.Key, identity.Location);

        config.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Audio24Khz160KBitRateMonoMp3);
        config.SpeechSynthesisVoiceName = "en-US-JennyNeural";

        using var audioConfig = AudioConfig.FromWavFileOutput(audio);

        using var synthesizer = new SpeechSynthesizer(config, audioConfig);

        await synthesizer.SpeakTextAsync(text);
    }
}