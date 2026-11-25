using System;
using System.Collections.Generic;
using FtsLib;

namespace KitveiHakodesh.Core.SeforimDbFullTextSearch
{
    /// <summary>
    /// What the searcher asks of a corpus to extend short hits over their neighbour lines: the
    /// lines at given (book id, lineIndex) positions. Implemented by the plain
    /// <see cref="SeforimDbFtsCorpus"/> and by <see cref="CompositeFtsCorpus"/>, whose book and
    /// line ids are the GLOBAL ids of <see cref="KitveiHakodesh.Core.SeforimDb.SeforimDbLibraryId"/>.
    /// </summary>
    internal interface ISeforimDbFtsPositionCorpus : IDisposable
    {
        /// <summary>The snippet texts of the given lines (and, for the short ones, their same-book neighbours).</summary>
        List<FtsSnippetText> FetchSnippetTexts(IReadOnlyList<long> lineIds, IReadOnlyCollection<long> shortLineIds);

        /// <summary>The index position of a line, from the books' blocks of positions; null when it cannot be placed.</summary>
        long? PositionOfLine(long lineId, Dictionary<long, (long First, long Count)> books);

        /// <summary>Each book's contiguous block of index positions: [First, First + Count - 1].</summary>
        Dictionary<long, (long First, long Count)> BookPositionRanges();

        /// <summary>The books as blocks in result order (see <see cref="IFtsOrderedCorpus.BlocksInResultOrder"/>); ids are the corpus's own.</summary>
        IReadOnlyList<(long BlockId, long Count)> BlocksInResultOrder();

        /// <summary>The placement (book id, lineIndex, own TOC entry) of each line, no text. An id with no line
        /// is absent. Ids, book ids and TOC entry ids are the corpus's own (global, for a composite).</summary>
        Dictionary<long, FtsDocumentPlacement> FetchPlacements(IReadOnlyList<long> ids);
    }
}
