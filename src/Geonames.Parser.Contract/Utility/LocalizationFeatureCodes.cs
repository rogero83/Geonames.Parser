using Geonames.Parser.Contract.Enums;

namespace Geonames.Parser.Contract.Utility;

/// <summary>
/// Feature codes essenziali per un database di città/localizzazione
/// </summary>
public static class LocalizationFeatureCodes
{
    /// <summary>
    /// Feature codes per luoghi abitati (ESSENZIALI per localizzazione)
    /// </summary>
    public static class PopulatedPlaces
    {
        /// <summary>
        /// Codici PRIMARI - da includere sempre
        /// </summary>
        public static readonly HashSet<GeonamesFeatureCode> Primary =
        [
            GeonamesFeatureCode.PPLC,   // Capitale nazionale
            GeonamesFeatureCode.PPLA,   // Capoluogo di regione/stato
            GeonamesFeatureCode.PPLA2,  // Capoluogo di provincia
            GeonamesFeatureCode.PPLA3,  // Capoluogo di comune
            GeonamesFeatureCode.PPLA4,  // Capoluogo di livello 4
            GeonamesFeatureCode.PPL,    // Luogo abitato generico
        ];

        /// <summary>
        /// Codici SECONDARI - includere in base alle esigenze
        /// </summary>
        public static readonly HashSet<GeonamesFeatureCode> Secondary =
        [
            GeonamesFeatureCode.PPLX,   // Sezione di città (quartieri)
            GeonamesFeatureCode.PPLL,   // Località abitata minore
            GeonamesFeatureCode.PPLF,   // Villaggio agricolo
        ];

        /// <summary>
        /// Codici STORICI - opzionali (per app storiche)
        /// </summary>
        public static readonly HashSet<GeonamesFeatureCode> Historical =
        [
            GeonamesFeatureCode.PPLCH,  // Ex capitale
            GeonamesFeatureCode.PPLH,   // Luogo abitato storico
            GeonamesFeatureCode.PPLQ,   // Luogo abbandonato
            GeonamesFeatureCode.PPLW,   // Luogo distrutto
        ];

        /// <summary>
        /// Tutti i codici di luoghi abitati
        /// </summary>
        public static readonly HashSet<GeonamesFeatureCode> All =
            Primary.Union(Secondary).Union(Historical).ToHashSet();
    }

    /// <summary>
    /// Feature codes amministrativi (per gerarchia)
    /// </summary>
    public static class Administrative
    {
        /// <summary>
        /// Divisioni amministrative attive
        /// </summary>
        public static readonly HashSet<GeonamesFeatureCode> Active =
        [
            GeonamesFeatureCode.PCLI,   // Paese indipendente
            GeonamesFeatureCode.ADM1,   // Divisione 1° ordine (Regione)
            GeonamesFeatureCode.ADM2,   // Divisione 2° ordine (Provincia)
            GeonamesFeatureCode.ADM3,   // Divisione 3° ordine (Comune)
            GeonamesFeatureCode.ADM4,   // Divisione 4° ordine
            GeonamesFeatureCode.ADM5,   // Divisione 5° ordine
        ];

        /// <summary>
        /// Divisioni amministrative storiche
        /// </summary>
        public static readonly HashSet<GeonamesFeatureCode> Historical = new()
        {
            GeonamesFeatureCode.PCLH,   // Ex paese
            GeonamesFeatureCode.ADM1H,  // Ex divisione 1° ordine
            GeonamesFeatureCode.ADM2H,  // Ex divisione 2° ordine
        };
    }
}