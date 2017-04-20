using System;
using Microsoft.Speech.Recognition;

namespace SpeechSampleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load grammar from xml file
            string grammarPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"grammars/grammar1.xml");
            var grammar = new Grammar(grammarPath);

            // Create an in-process speech recognizer for the en-US locale.
            SpeechRecognitionEngine speechRecognitionEngine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

            // Create and load a dictation grammar.
            speechRecognitionEngine.LoadGrammar(grammar);

            // Configure input to the speech recognizer.
            speechRecognitionEngine.SetInputToDefaultAudioDevice();

            // Register an event handler when speech recognized
            speechRecognitionEngine.SpeechRecognized += 
                new EventHandler<SpeechRecognizedEventArgs>(onSpeechRecognized);

            // Start recognition.
            speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);

            // Keep the console window open.
            Console.ReadLine();
        }

        static private void onSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Speech recognized:  " + e.Result.Text);
            Console.WriteLine();
            Console.WriteLine("Semantic results:");
            Console.WriteLine("  The flight origin is " + e.Result.Semantics["origin"].Value);
            Console.WriteLine("  The flight destination is " + e.Result.Semantics["destination"].Value);
            Console.WriteLine("\n\n");
        }
    }
}