using System;
using System.Collections.Generic;
using Avalonia.Media;
using AvaloniaEdit.Document;
using TextMateSharp.Model;

namespace AvaloniaEdit.TextMate
{
    public abstract class TextTransformation : TextSegment
    {
        public TextTransformation(int startOffset, int endOffset)
        {
            StartOffset = startOffset;
            EndOffset = endOffset;
        }

        public abstract void Transform(GenericLineTransformer transformer, DocumentLine line);
    }

    public class ForegroundTextTransformation : TextTransformation
    {
        private readonly Dictionary<int, IBrush> _brushCache;
        
        public ForegroundTextTransformation(Dictionary<int, IBrush> brushCache, int startOffset, int endOffset, int brushId) : base(startOffset, endOffset)
        {
            _brushCache = brushCache;
            Foreground = brushId;
        }

        public int Foreground { get; set; }

        public override void Transform(GenericLineTransformer transformer, DocumentLine line)
        {
            if (Length == 0)
            {
                return;
            }

            if (!_brushCache.ContainsKey(Foreground))
            {
                return;
            }

            var formattedOffset = 0;
            var endOffset = line.EndOffset;

            if (StartOffset > line.Offset)
            {
                formattedOffset = StartOffset - line.Offset;
            }

            if (EndOffset < line.EndOffset)
            {
                endOffset = EndOffset;
            }

            transformer.SetTextStyle(line, formattedOffset, endOffset - line.Offset - formattedOffset, _brushCache[Foreground]);
        }
    }
}