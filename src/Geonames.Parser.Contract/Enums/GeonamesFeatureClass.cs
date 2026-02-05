namespace Geonames.Parser.Contract.Enums;

/// <summary>
/// Categorie principali dei feature codes GeoNames
/// </summary>
public enum GeonamesFeatureClass
{
    /// <summary>A - country, state, region (paese, stato, regione)</summary>
    A,

    /// <summary>H - stream, lake (corsi d'acqua, laghi)</summary>
    H,

    /// <summary>L - parks, area (parchi, aree)</summary>
    L,

    /// <summary>P - city, village (città, villaggi)</summary>
    P,

    /// <summary>R - road, railroad (strade, ferrovie)</summary>
    R,

    /// <summary>S - spot, building, farm (punti, edifici, fattorie)</summary>
    S,

    /// <summary>T - mountain, hill, rock (montagne, colline, rocce)</summary>
    T,

    /// <summary>U - undersea (caratteristiche sottomarine)</summary>
    U,

    /// <summary>V - forest, heath (foreste, vegetazione)</summary>
    V
}
