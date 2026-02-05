namespace Geonames.Parser.Contract.Enums;

/// <summary>
/// GeoNames Feature Codes - Codici per classificare le caratteristiche geografiche
/// </summary>
public enum GeonamesFeatureCode
{
    // ===== A - country, state, region (Paesi, stati, regioni) =====

    /// <summary>ADM1 - first-order administrative division (divisione amministrativa di primo ordine, es. stato in USA)</summary>
    ADM1,

    /// <summary>ADM1H - historical first-order administrative division (ex divisione amministrativa di primo ordine)</summary>
    ADM1H,

    /// <summary>ADM2 - second-order administrative division (divisione amministrativa di secondo ordine)</summary>
    ADM2,

    /// <summary>ADM2H - historical second-order administrative division (ex divisione amministrativa di secondo ordine)</summary>
    ADM2H,

    /// <summary>ADM3 - third-order administrative division (divisione amministrativa di terzo ordine)</summary>
    ADM3,

    /// <summary>ADM3H - historical third-order administrative division (ex divisione amministrativa di terzo ordine)</summary>
    ADM3H,

    /// <summary>ADM4 - fourth-order administrative division (divisione amministrativa di quarto ordine)</summary>
    ADM4,

    /// <summary>ADM4H - historical fourth-order administrative division (ex divisione amministrativa di quarto ordine)</summary>
    ADM4H,

    /// <summary>ADM5 - fifth-order administrative division (divisione amministrativa di quinto ordine)</summary>
    ADM5,

    /// <summary>ADM5H - historical fifth-order administrative division (ex divisione amministrativa di quinto ordine)</summary>
    ADM5H,

    /// <summary>ADMD - administrative division (divisione amministrativa generica)</summary>
    ADMD,

    /// <summary>ADMDH - historical administrative division (ex divisione amministrativa)</summary>
    ADMDH,

    /// <summary>LTER - leased area (area in affitto, solitamente per installazioni militari)</summary>
    LTER,

    /// <summary>PCL - political entity (entità politica)</summary>
    PCL,

    /// <summary>PCLD - dependent political entity (entità politica dipendente)</summary>
    PCLD,

    /// <summary>PCLF - freely associated state (stato liberamente associato)</summary>
    PCLF,

    /// <summary>PCLH - historical political entity (ex entità politica)</summary>
    PCLH,

    /// <summary>PCLI - independent political entity (entità politica indipendente)</summary>
    PCLI,

    /// <summary>PCLIX - section of independent political entity (sezione di entità politica indipendente)</summary>
    PCLIX,

    /// <summary>PCLS - semi-independent political entity (entità politica semi-indipendente)</summary>
    PCLS,

    /// <summary>PRSH - parish (parrocchia, distretto ecclesiastico)</summary>
    PRSH,

    /// <summary>TERR - territory (territorio)</summary>
    TERR,

    /// <summary>ZN - zone (zona)</summary>
    ZN,

    /// <summary>ZNB - buffer zone (zona cuscinetto tra due nazioni)</summary>
    ZNB,

    // ===== H - stream, lake (Corsi d'acqua, laghi) =====

    /// <summary>AIRS - seaplane landing area (area di atterraggio per idrovolanti)</summary>
    AIRS,

    /// <summary>ANCH - anchorage (ancoraggio)</summary>
    ANCH,

    /// <summary>BAY - bay (baia)</summary>
    BAY,

    /// <summary>BAYS - bays (baie)</summary>
    BAYS,

    /// <summary>BGHT - bight(s) (insenatura aperta)</summary>
    BGHT,

    /// <summary>BNK - bank(s) (banco, elevazione sommersa)</summary>
    BNK,

    /// <summary>BNKR - stream bank (argine di fiume)</summary>
    BNKR,

    /// <summary>BNKX - section of bank (sezione di argine)</summary>
    BNKX,

    /// <summary>BOG - bog(s) (palude torbosa)</summary>
    BOG,

    /// <summary>CAPG - icecap (calotta di ghiaccio)</summary>
    CAPG,

    /// <summary>CHN - channel (canale naturale)</summary>
    CHN,

    /// <summary>CHNL - lake channel(s) (canale lacustre)</summary>
    CHNL,

    /// <summary>CHNM - marine channel (canale marino)</summary>
    CHNM,

    /// <summary>CHNN - navigation channel (canale di navigazione segnalato)</summary>
    CHNN,

    /// <summary>CNFL - confluence (confluenza di corsi d'acqua)</summary>
    CNFL,

    /// <summary>CNL - canal (canale artificiale)</summary>
    CNL,

    /// <summary>CNLA - aqueduct (acquedotto)</summary>
    CNLA,

    /// <summary>CNLB - canal bend (curva di canale)</summary>
    CNLB,

    /// <summary>CNLD - drainage canal (canale di drenaggio)</summary>
    CNLD,

    /// <summary>CNLI - irrigation canal (canale di irrigazione)</summary>
    CNLI,

    /// <summary>CNLN - navigation canal(s) (canale navigabile)</summary>
    CNLN,

    /// <summary>CNLQ - abandoned canal (canale abbandonato)</summary>
    CNLQ,

    /// <summary>CNLSB - underground irrigation canal(s) (canale di irrigazione sotterraneo)</summary>
    CNLSB,

    /// <summary>CNLX - section of canal (sezione di canale)</summary>
    CNLX,

    /// <summary>COVE - cove(s) (insenatura piccola)</summary>
    COVE,

    /// <summary>CRKT - tidal creek(s) (canale di marea)</summary>
    CRKT,

    /// <summary>CRNT - current (corrente marina)</summary>
    CRNT,

    /// <summary>CUTF - cutoff (canale formato da taglio di meandro)</summary>
    CUTF,

    /// <summary>DCK - dock(s) (bacino portuale)</summary>
    DCK,

    /// <summary>DCKB - docking basin (bacino di attracco)</summary>
    DCKB,

    /// <summary>DOMG - icecap dome (cupola di calotta glaciale)</summary>
    DOMG,

    /// <summary>DPRG - icecap depression (depressione di calotta glaciale)</summary>
    DPRG,

    /// <summary>DTCH - ditch (fossato)</summary>
    DTCH,

    /// <summary>DTCHD - drainage ditch (fossato di drenaggio)</summary>
    DTCHD,

    /// <summary>DTCHI - irrigation ditch (fossato di irrigazione)</summary>
    DTCHI,

    /// <summary>DTCHM - ditch mouth(s) (sbocco di fossato)</summary>
    DTCHM,

    /// <summary>ESTY - estuary (estuario)</summary>
    ESTY,

    /// <summary>FISH - fishing area (area di pesca)</summary>
    FISH,

    /// <summary>FJD - fjord (fiordo)</summary>
    FJD,

    /// <summary>FJDS - fjords (fiordi)</summary>
    FJDS,

    /// <summary>FLLS - waterfall(s) (cascata)</summary>
    FLLS,

    /// <summary>FLLSX - section of waterfall(s) (sezione di cascata)</summary>
    FLLSX,

    /// <summary>FLTM - mud flat(s) (distesa fangosa)</summary>
    FLTM,

    /// <summary>FLTT - tidal flat(s) (piana di marea)</summary>
    FLTT,

    /// <summary>GLCR - glacier(s) (ghiacciaio)</summary>
    GLCR,

    /// <summary>GULF - gulf (golfo)</summary>
    GULF,

    /// <summary>GYSR - geyser (geyser)</summary>
    GYSR,

    /// <summary>HBR - harbor(s) (porto naturale)</summary>
    HBR,

    /// <summary>HBRX - section of harbor (sezione di porto)</summary>
    HBRX,

    /// <summary>INLT - inlet (insenatura stretta)</summary>
    INLT,

    /// <summary>INLTQ - former inlet (ex insenatura)</summary>
    INLTQ,

    /// <summary>LBED - lake bed(s) (letto di lago prosciugato)</summary>
    LBED,

    /// <summary>LGN - lagoon (laguna)</summary>
    LGN,

    /// <summary>LGNS - lagoons (lagune)</summary>
    LGNS,

    /// <summary>LGNX - section of lagoon (sezione di laguna)</summary>
    LGNX,

    /// <summary>LK - lake (lago)</summary>
    LK,

    /// <summary>LKC - crater lake (lago craterico)</summary>
    LKC,

    /// <summary>LKI - intermittent lake (lago intermittente)</summary>
    LKI,

    /// <summary>LKN - salt lake (lago salato)</summary>
    LKN,

    /// <summary>LKNI - intermittent salt lake (lago salato intermittente)</summary>
    LKNI,

    /// <summary>LKO - oxbow lake (lago a meandro)</summary>
    LKO,

    /// <summary>LKOI - intermittent oxbow lake (lago a meandro intermittente)</summary>
    LKOI,

    /// <summary>LKS - lakes (laghi)</summary>
    LKS,

    /// <summary>LKSB - underground lake (lago sotterraneo)</summary>
    LKSB,

    /// <summary>LKSC - crater lakes (laghi craterici)</summary>
    LKSC,

    /// <summary>LKSI - intermittent lakes (laghi intermittenti)</summary>
    LKSI,

    /// <summary>LKSN - salt lakes (laghi salati)</summary>
    LKSN,

    /// <summary>LKSNI - intermittent salt lakes (laghi salati intermittenti)</summary>
    LKSNI,

    /// <summary>LKX - section of lake (sezione di lago)</summary>
    LKX,

    /// <summary>MFGN - salt evaporation ponds (vasche di evaporazione sale)</summary>
    MFGN,

    /// <summary>MGV - mangrove swamp (palude di mangrovie)</summary>
    MGV,

    /// <summary>MOOR - moor(s) (brughiera)</summary>
    MOOR,

    /// <summary>MRSH - marsh(es) (palude erbacea)</summary>
    MRSH,

    /// <summary>MRSHN - salt marsh (palude salmastra)</summary>
    MRSHN,

    /// <summary>NRWS - narrows (stretto navigabile)</summary>
    NRWS,

    /// <summary>OCN - ocean (oceano)</summary>
    OCN,

    /// <summary>OVF - overfalls (area di onde frangenti)</summary>
    OVF,

    /// <summary>PND - pond (stagno)</summary>
    PND,

    /// <summary>PNDI - intermittent pond (stagno intermittente)</summary>
    PNDI,

    /// <summary>PNDN - salt pond (stagno salato)</summary>
    PNDN,

    /// <summary>PNDNI - intermittent salt pond(s) (stagno salato intermittente)</summary>
    PNDNI,

    /// <summary>PNDS - ponds (stagni)</summary>
    PNDS,

    /// <summary>PNDSF - fishponds (vivai ittici)</summary>
    PNDSF,

    /// <summary>PNDSI - intermittent ponds (stagni intermittenti)</summary>
    PNDSI,

    /// <summary>PNDSN - salt ponds (stagni salati)</summary>
    PNDSN,

    /// <summary>POOL - pool(s) (pozza d'acqua)</summary>
    POOL,

    /// <summary>POOLI - intermittent pool (pozza intermittente)</summary>
    POOLI,

    /// <summary>RCH - reach (tratto rettilineo navigabile)</summary>
    RCH,

    /// <summary>RDGG - icecap ridge (cresta di calotta glaciale)</summary>
    RDGG,

    /// <summary>RDST - roadstead (rada aperta)</summary>
    RDST,

    /// <summary>RF - reef(s) (scogliera)</summary>
    RF,

    /// <summary>RFC - coral reef(s) (barriera corallina)</summary>
    RFC,

    /// <summary>RFX - section of reef (sezione di scogliera)</summary>
    RFX,

    /// <summary>RPDS - rapids (rapide)</summary>
    RPDS,

    /// <summary>RSV - reservoir(s) (bacino idrico artificiale)</summary>
    RSV,

    /// <summary>RSVI - intermittent reservoir (bacino intermittente)</summary>
    RSVI,

    /// <summary>RSVT - water tank (serbatoio d'acqua)</summary>
    RSVT,

    /// <summary>RVN - ravine(s) (burrone)</summary>
    RVN,

    /// <summary>SBKH - sabkha(s) (pianura salata soggetta a inondazioni)</summary>
    SBKH,

    /// <summary>SD - sound (braccio di mare)</summary>
    SD,

    /// <summary>SEA - sea (mare)</summary>
    SEA,

    /// <summary>SHOL - shoal(s) (bassofondo)</summary>
    SHOL,

    /// <summary>SILL - sill (soglia sottomarina)</summary>
    SILL,

    /// <summary>SPNG - spring(s) (sorgente)</summary>
    SPNG,

    /// <summary>SPNS - sulphur spring(s) (sorgente sulfurea)</summary>
    SPNS,

    /// <summary>SPNT - hot spring(s) (sorgente termale)</summary>
    SPNT,

    /// <summary>STM - stream (corso d'acqua)</summary>
    STM,

    /// <summary>STMA - anabranch (ramo divergente di fiume)</summary>
    STMA,

    /// <summary>STMB - stream bend (curva di fiume)</summary>
    STMB,

    /// <summary>STMC - canalized stream (fiume canalizzato)</summary>
    STMC,

    /// <summary>STMD - distributary(-ies) (distributario di delta)</summary>
    STMD,

    /// <summary>STMH - headwaters (sorgenti di fiume)</summary>
    STMH,

    /// <summary>STMI - intermittent stream (corso d'acqua intermittente)</summary>
    STMI,

    /// <summary>STMIX - section of intermittent stream (sezione di corso intermittente)</summary>
    STMIX,

    /// <summary>STMM - stream mouth(s) (foce)</summary>
    STMM,

    /// <summary>STMQ - abandoned watercourse (ex corso d'acqua)</summary>
    STMQ,

    /// <summary>STMS - streams (corsi d'acqua)</summary>
    STMS,

    /// <summary>STMSB - lost river (fiume che scompare sottoterra)</summary>
    STMSB,

    /// <summary>STMX - section of stream (sezione di fiume)</summary>
    STMX,

    /// <summary>STRT - strait (stretto marino)</summary>
    STRT,

    /// <summary>SWMP - swamp (palude alberata)</summary>
    SWMP,

    /// <summary>SYSI - irrigation system (sistema di irrigazione)</summary>
    SYSI,

    /// <summary>TNLC - canal tunnel (tunnel per canale)</summary>
    TNLC,

    /// <summary>WAD - wadi (letto di fiume stagionale desertico)</summary>
    WAD,

    /// <summary>WADB - wadi bend (curva di wadi)</summary>
    WADB,

    /// <summary>WADJ - wadi junction (confluenza di wadi)</summary>
    WADJ,

    /// <summary>WADM - wadi mouth (sbocco di wadi)</summary>
    WADM,

    /// <summary>WADS - wadies (wadi)</summary>
    WADS,

    /// <summary>WADX - section of wadi (sezione di wadi)</summary>
    WADX,

    /// <summary>WHRL - whirlpool (vortice d'acqua)</summary>
    WHRL,

    /// <summary>WLL - well (pozzo)</summary>
    WLL,

    /// <summary>WLLQ - abandoned well (pozzo abbandonato)</summary>
    WLLQ,

    /// <summary>WLLS - wells (pozzi)</summary>
    WLLS,

    /// <summary>WTLD - wetland (zona umida)</summary>
    WTLD,

    /// <summary>WTLDI - intermittent wetland (zona umida intermittente)</summary>
    WTLDI,

    /// <summary>WTRC - watercourse (corso d'acqua naturale o artificiale)</summary>
    WTRC,

    /// <summary>WTRH - waterhole(s) (pozza d'acqua naturale)</summary>
    WTRH,

    // ===== L - parks, area (Parchi, aree) =====

    /// <summary>AGRC - agricultural colony (colonia agricola)</summary>
    AGRC,

    /// <summary>AMUS - amusement park (parco divertimenti)</summary>
    AMUS,

    /// <summary>AREA - area (area senza caratteristiche omogenee)</summary>
    AREA,

    /// <summary>BSND - drainage basin (bacino di drenaggio)</summary>
    BSND,

    /// <summary>BSNP - petroleum basin (bacino petrolifero)</summary>
    BSNP,

    /// <summary>BTL - battlefield (campo di battaglia storico)</summary>
    BTL,

    /// <summary>CLG - clearing (radura in foresta)</summary>
    CLG,

    /// <summary>CMN - common (parco o pascolo comunale)</summary>
    CMN,

    /// <summary>CNS - concession area (area in concessione)</summary>
    CNS,

    /// <summary>COLF - coalfield (giacimento carbonifero)</summary>
    COLF,

    /// <summary>CONT - continent (continente)</summary>
    CONT,

    /// <summary>CST - coast (costa)</summary>
    CST,

    /// <summary>CTRB - business center (centro direzionale)</summary>
    CTRB,

    /// <summary>DEVH - housing development (complesso residenziale)</summary>
    DEVH,

    /// <summary>FLD - field(s) (campo aperto)</summary>
    FLD,

    /// <summary>FLDI - irrigated field(s) (campo irrigato)</summary>
    FLDI,

    /// <summary>GASF - gasfield (giacimento di gas)</summary>
    GASF,

    /// <summary>GRAZ - grazing area (area di pascolo)</summary>
    GRAZ,

    /// <summary>GVL - gravel area (area ghiaiosa)</summary>
    GVL,

    /// <summary>INDS - industrial area (area industriale)</summary>
    INDS,

    /// <summary>LAND - arctic land (terra artica)</summary>
    LAND,

    /// <summary>LCTY - locality (località di carattere misto)</summary>
    LCTY,

    /// <summary>MILB - military base (base militare)</summary>
    MILB,

    /// <summary>MNA - mining area (area mineraria)</summary>
    MNA,

    /// <summary>MVA - maneuver area (area di manovre militari)</summary>
    MVA,

    /// <summary>NVB - naval base (base navale)</summary>
    NVB,

    /// <summary>OAS - oasis(-es) (oasi)</summary>
    OAS,

    /// <summary>OILF - oilfield (giacimento petrolifero)</summary>
    OILF,

    /// <summary>PEAT - peat cutting area (area di estrazione torba)</summary>
    PEAT,

    /// <summary>PRK - park (parco)</summary>
    PRK,

    /// <summary>PRT - port (porto commerciale)</summary>
    PRT,

    /// <summary>QCKS - quicksand (sabbie mobili)</summary>
    QCKS,

    /// <summary>RES - reserve (riserva)</summary>
    RES,

    /// <summary>RESA - agricultural reserve (riserva agricola)</summary>
    RESA,

    /// <summary>RESF - forest reserve (riserva forestale)</summary>
    RESF,

    /// <summary>RESH - hunting reserve (riserva di caccia)</summary>
    RESH,

    /// <summary>RESN - nature reserve (riserva naturale)</summary>
    RESN,

    /// <summary>RESP - palm tree reserve (riserva di palme)</summary>
    RESP,

    /// <summary>RESV - reservation (riserva indigena)</summary>
    RESV,

    /// <summary>RESW - wildlife reserve (riserva faunistica)</summary>
    RESW,

    /// <summary>RGN - region (regione)</summary>
    RGN,

    /// <summary>RGNE - economic region (regione economica)</summary>
    RGNE,

    /// <summary>RGNH - historical region (regione storica)</summary>
    RGNH,

    /// <summary>RGNL - lake region (regione lacustre)</summary>
    RGNL,

    /// <summary>RNGA - artillery range (campo di tiro artiglieria)</summary>
    RNGA,

    /// <summary>SALT - salt area (area salina)</summary>
    SALT,

    /// <summary>SNOW - snowfield (campo di neve permanente)</summary>
    SNOW,

    /// <summary>TRB - tribal area (area tribale)</summary>
    TRB,

    // ===== P - city, village (Città, villaggi) =====

    /// <summary>PPL - populated place (luogo abitato generico)</summary>
    PPL,

    /// <summary>PPLA - seat of a first-order administrative division (capoluogo di divisione di primo ordine)</summary>
    PPLA,

    /// <summary>PPLA2 - seat of a second-order administrative division (capoluogo di divisione di secondo ordine)</summary>
    PPLA2,

    /// <summary>PPLA3 - seat of a third-order administrative division (capoluogo di divisione di terzo ordine)</summary>
    PPLA3,

    /// <summary>PPLA4 - seat of a fourth-order administrative division (capoluogo di divisione di quarto ordine)</summary>
    PPLA4,

    /// <summary>PPLA5 - seat of a fifth-order administrative division (capoluogo di divisione di quinto ordine)</summary>
    PPLA5,

    /// <summary>PPLC - capital of a political entity (capitale)</summary>
    PPLC,

    /// <summary>PPLCH - historical capital of a political entity (ex capitale)</summary>
    PPLCH,

    /// <summary>PPLF - farm village (villaggio agricolo)</summary>
    PPLF,

    /// <summary>PPLG - seat of government of a political entity (sede di governo)</summary>
    PPLG,

    /// <summary>PPLH - historical populated place (luogo abitato storico)</summary>
    PPLH,

    /// <summary>PPLL - populated locality (località abitata minore)</summary>
    PPLL,

    /// <summary>PPLQ - abandoned populated place (luogo abitato abbandonato)</summary>
    PPLQ,

    /// <summary>PPLR - religious populated place (luogo abitato religioso)</summary>
    PPLR,

    /// <summary>PPLS - populated places (luoghi abitati)</summary>
    PPLS,

    /// <summary>PPLW - destroyed populated place (luogo abitato distrutto)</summary>
    PPLW,

    /// <summary>PPLX - section of populated place (sezione di luogo abitato)</summary>
    PPLX,

    /// <summary>STLMT - israeli settlement (insediamento israeliano)</summary>
    STLMT,

    // ===== R - road, railroad (Strade, ferrovie) =====

    /// <summary>CSWY - causeway (strada rialzata su acqua/palude)</summary>
    CSWY,

    /// <summary>OILP - oil pipeline (oleodotto)</summary>
    OILP,

    /// <summary>PRMN - promenade (lungomare pedonale)</summary>
    PRMN,

    /// <summary>PTGE - portage (trasporto terrestre tra vie navigabili)</summary>
    PTGE,

    /// <summary>RD - road (strada)</summary>
    RD,

    /// <summary>RDA - ancient road (strada antica)</summary>
    RDA,

    /// <summary>RDB - road bend (curva stradale)</summary>
    RDB,

    /// <summary>RDCUT - road cut (taglio stradale in collina)</summary>
    RDCUT,

    /// <summary>RDJCT - road junction (incrocio stradale)</summary>
    RDJCT,

    /// <summary>RJCT - railroad junction (incrocio ferroviario)</summary>
    RJCT,

    /// <summary>RR - railroad (ferrovia)</summary>
    RR,

    /// <summary>RRQ - abandoned railroad (ferrovia abbandonata)</summary>
    RRQ,

    /// <summary>RTE - caravan route (rotta carovaniera)</summary>
    RTE,

    /// <summary>RYD - railroad yard (scalo ferroviario)</summary>
    RYD,

    /// <summary>ST - street (strada urbana)</summary>
    ST,

    /// <summary>STKR - stock route (percorso bestiame)</summary>
    STKR,

    /// <summary>TNL - tunnel (tunnel generico)</summary>
    TNL,

    /// <summary>TNLN - natural tunnel (tunnel naturale)</summary>
    TNLN,

    /// <summary>TNLRD - road tunnel (tunnel stradale)</summary>
    TNLRD,

    /// <summary>TNLRR - railroad tunnel (tunnel ferroviario)</summary>
    TNLRR,

    /// <summary>TNLS - tunnels (tunnel)</summary>
    TNLS,

    /// <summary>TRL - trail (sentiero)</summary>
    TRL,

    // ===== S - spot, building, farm (Punti, edifici, fattorie) =====

    /// <summary>ADMF - administrative facility (edificio amministrativo)</summary>
    ADMF,

    /// <summary>AGRF - agricultural facility (struttura agricola)</summary>
    AGRF,

    /// <summary>AIRB - airbase (base aerea)</summary>
    AIRB,

    /// <summary>AIRF - airfield (campo di aviazione)</summary>
    AIRF,

    /// <summary>AIRH - heliport (eliporto)</summary>
    AIRH,

    /// <summary>AIRP - airport (aeroporto)</summary>
    AIRP,

    /// <summary>AIRQ - abandoned airfield (campo aviazione abbandonato)</summary>
    AIRQ,

    /// <summary>AIRT - terminal (terminal aeroportuale)</summary>
    AIRT,

    /// <summary>AMTH - amphitheater (anfiteatro)</summary>
    AMTH,

    /// <summary>ANS - archaeological/prehistoric site (sito archeologico)</summary>
    ANS,

    /// <summary>AQC - aquaculture facility (impianto di acquacoltura)</summary>
    AQC,

    /// <summary>ARCH - arch (arco naturale o artificiale)</summary>
    ARCH,

    /// <summary>ARCHV - archive (archivio)</summary>
    ARCHV,

    /// <summary>ART - piece of art (opera d'arte)</summary>
    ART,

    /// <summary>ASTR - astronomical station (stazione astronomica)</summary>
    ASTR,

    /// <summary>ASYL - asylum (manicomio)</summary>
    ASYL,

    /// <summary>ATHF - athletic field (campo sportivo)</summary>
    ATHF,

    /// <summary>ATM - automatic teller machine (bancomat)</summary>
    ATM,

    /// <summary>BANK - bank (banca)</summary>
    BANK,

    /// <summary>BCN - beacon (faro segnalatore)</summary>
    BCN,

    /// <summary>BDG - bridge (ponte)</summary>
    BDG,

    /// <summary>BDGQ - ruined bridge (ponte in rovina)</summary>
    BDGQ,

    /// <summary>BLDA - apartment building (condominio)</summary>
    BLDA,

    /// <summary>BLDG - building(s) (edificio generico)</summary>
    BLDG,

    /// <summary>BLDO - office building (edificio per uffici)</summary>
    BLDO,

    /// <summary>BP - boundary marker (cippo di confine)</summary>
    BP,

    /// <summary>BRKS - barracks (caserma)</summary>
    BRKS,

    /// <summary>BRKW - breakwater (frangiflutti)</summary>
    BRKW,

    /// <summary>BSTN - baling station (stazione di imballaggio)</summary>
    BSTN,

    /// <summary>BTYD - boatyard (cantiere navale piccolo)</summary>
    BTYD,

    /// <summary>BUR - burial cave(s) (grotta sepolcrale)</summary>
    BUR,

    /// <summary>BUSTN - bus station (stazione autobus)</summary>
    BUSTN,

    /// <summary>BUSTP - bus stop (fermata autobus)</summary>
    BUSTP,

    /// <summary>CARN - cairn (cumulo di pietre)</summary>
    CARN,

    /// <summary>CAVE - cave(s) (grotta)</summary>
    CAVE,

    /// <summary>CH - church (chiesa)</summary>
    CH,

    /// <summary>CMP - camp(s) (campo temporaneo)</summary>
    CMP,

    /// <summary>CMPL - logging camp (campo per boscaioli)</summary>
    CMPL,

    /// <summary>CMPLA - labor camp (campo per lavoratori)</summary>
    CMPLA,

    /// <summary>CMPMN - mining camp (campo minerario)</summary>
    CMPMN,

    /// <summary>CMPO - oil camp (campo petrolifero)</summary>
    CMPO,

    /// <summary>CMPQ - abandoned camp (campo abbandonato)</summary>
    CMPQ,

    /// <summary>CMPRF - refugee camp (campo profughi)</summary>
    CMPRF,

    /// <summary>CMTY - cemetery (cimitero)</summary>
    CMTY,

    /// <summary>COMC - communication center (centro comunicazioni)</summary>
    COMC,

    /// <summary>CRRL - corral(s) (recinto per bestiame)</summary>
    CRRL,

    /// <summary>CSNO - casino (casinò)</summary>
    CSNO,

    /// <summary>CSTL - castle (castello)</summary>
    CSTL,

    /// <summary>CSTM - customs house (dogana)</summary>
    CSTM,

    /// <summary>CTHSE - courthouse (tribunale)</summary>
    CTHSE,

    /// <summary>CTRA - atomic center (centro atomico)</summary>
    CTRA,

    /// <summary>CTRCM - community center (centro comunitario)</summary>
    CTRCM,

    /// <summary>CTRF - facility center (centro polifunzionale)</summary>
    CTRF,

    /// <summary>CTRM - medical center (centro medico)</summary>
    CTRM,

    /// <summary>CTRR - religious center (centro religioso)</summary>
    CTRR,

    /// <summary>CTRS - space center (centro spaziale)</summary>
    CTRS,

    /// <summary>CVNT - convent (convento)</summary>
    CVNT,

    /// <summary>DAM - dam (diga)</summary>
    DAM,

    /// <summary>DAMQ - ruined dam (diga in rovina)</summary>
    DAMQ,

    /// <summary>DAMSB - sub-surface dam (diga sotterranea)</summary>
    DAMSB,

    /// <summary>DARY - dairy (caseificio)</summary>
    DARY,

    /// <summary>DCKD - dry dock (bacino di carenaggio)</summary>
    DCKD,

    /// <summary>DCKY - dockyard (cantiere navale)</summary>
    DCKY,

    /// <summary>DIKE - dike (argine artificiale)</summary>
    DIKE,

    /// <summary>DIP - diplomatic facility (struttura diplomatica)</summary>
    DIP,

    /// <summary>DPOF - fuel depot (deposito carburante)</summary>
    DPOF,

    /// <summary>EST - estate(s) (tenuta agricola)</summary>
    EST,

    /// <summary>ESTO - oil palm plantation (piantagione palma da olio)</summary>
    ESTO,

    /// <summary>ESTR - rubber plantation (piantagione gomma)</summary>
    ESTR,

    /// <summary>ESTSG - sugar plantation (piantagione canna da zucchero)</summary>
    ESTSG,

    /// <summary>ESTT - tea plantation (piantagione tè)</summary>
    ESTT,

    /// <summary>ESTX - section of estate (sezione di tenuta)</summary>
    ESTX,

    /// <summary>FCL - facility (struttura generica)</summary>
    FCL,

    /// <summary>FNDY - foundry (fonderia)</summary>
    FNDY,

    /// <summary>FRM - farm (fattoria)</summary>
    FRM,

    /// <summary>FRMQ - abandoned farm (fattoria abbandonata)</summary>
    FRMQ,

    /// <summary>FRMS - farms (fattorie)</summary>
    FRMS,

    /// <summary>FRMT - farmstead (podere)</summary>
    FRMT,

    /// <summary>FT - fort (forte militare)</summary>
    FT,

    /// <summary>FY - ferry (traghetto)</summary>
    FY,

    /// <summary>FYT - ferry terminal (terminal traghetti)</summary>
    FYT,

    /// <summary>GATE - gate (cancello controllato)</summary>
    GATE,

    /// <summary>GDN - garden(s) (giardino)</summary>
    GDN,

    /// <summary>GHAT - ghat (scalinata sacra indiana)</summary>
    GHAT,

    /// <summary>GHSE - guest house (pensione)</summary>
    GHSE,

    /// <summary>GOSP - gas-oil separator plant (impianto separazione gas-olio)</summary>
    GOSP,

    /// <summary>GOVL - local government office (ufficio amministrazione locale)</summary>
    GOVL,

    /// <summary>GRVE - grave (tomba)</summary>
    GRVE,

    /// <summary>HERM - hermitage (eremo)</summary>
    HERM,

    /// <summary>HLT - halting place (luogo di sosta carovane)</summary>
    HLT,

    /// <summary>HMSD - homestead (fattoria isolata Australia/NZ)</summary>
    HMSD,

    /// <summary>HSE - house(s) (casa)</summary>
    HSE,

    /// <summary>HSEC - country house (villa di campagna)</summary>
    HSEC,

    /// <summary>HSP - hospital (ospedale)</summary>
    HSP,

    /// <summary>HSPC - clinic (clinica ambulatoriale)</summary>
    HSPC,

    /// <summary>HSPD - dispensary (dispensario medico)</summary>
    HSPD,

    /// <summary>HSPL - leprosarium (lebbrosario)</summary>
    HSPL,

    /// <summary>HSTS - historical site (sito storico)</summary>
    HSTS,

    /// <summary>HTL - hotel (hotel)</summary>
    HTL,

    /// <summary>HUT - hut (capanna)</summary>
    HUT,

    /// <summary>HUTS - huts (capanne)</summary>
    HUTS,

    /// <summary>INSM - military installation (installazione militare)</summary>
    INSM,

    /// <summary>ITTR - research institute (istituto di ricerca)</summary>
    ITTR,

    /// <summary>JTY - jetty (molo)</summary>
    JTY,

    /// <summary>LDNG - landing (approdo)</summary>
    LDNG,

    /// <summary>LEPC - leper colony (colonia lebbrosi)</summary>
    LEPC,

    /// <summary>LIBR - library (biblioteca)</summary>
    LIBR,

    /// <summary>LNDF - landfill (discarica)</summary>
    LNDF,

    /// <summary>LOCK - lock(s) (chiusa fluviale)</summary>
    LOCK,

    /// <summary>LTHSE - lighthouse (faro)</summary>
    LTHSE,

    /// <summary>MALL - mall (centro commerciale)</summary>
    MALL,

    /// <summary>MAR - marina (porto turistico)</summary>
    MAR,

    /// <summary>MFG - factory (fabbrica)</summary>
    MFG,

    /// <summary>MFGB - brewery (birreria)</summary>
    MFGB,

    /// <summary>MFGC - cannery (stabilimento inscatolamento)</summary>
    MFGC,

    /// <summary>MFGCU - copper works (impianto lavorazione rame)</summary>
    MFGCU,

    /// <summary>MFGLM - limekiln (fornace per calce)</summary>
    MFGLM,

    /// <summary>MFGM - munitions plant (fabbrica munizioni)</summary>
    MFGM,

    /// <summary>MFGPH - phosphate works (impianto fosfati)</summary>
    MFGPH,

    /// <summary>MFGQ - abandoned factory (fabbrica abbandonata)</summary>
    MFGQ,

    /// <summary>MFGSG - sugar refinery (raffineria zucchero)</summary>
    MFGSG,

    /// <summary>MKT - market (mercato)</summary>
    MKT,

    /// <summary>ML - mill(s) (mulino)</summary>
    ML,

    /// <summary>MLM - ore treatment plant (impianto trattamento minerale)</summary>
    MLM,

    /// <summary>MLO - olive oil mill (frantoio)</summary>
    MLO,

    /// <summary>MLSG - sugar mill (zuccherificio)</summary>
    MLSG,

    /// <summary>MLSGQ - former sugar mill (ex zuccherificio)</summary>
    MLSGQ,

    /// <summary>MLSW - sawmill (segheria)</summary>
    MLSW,

    /// <summary>MLWND - windmill (mulino a vento)</summary>
    MLWND,

    /// <summary>MLWTR - water mill (mulino ad acqua)</summary>
    MLWTR,

    /// <summary>MN - mine(s) (miniera)</summary>
    MN,

    /// <summary>MNAU - gold mine(s) (miniera d'oro)</summary>
    MNAU,

    /// <summary>MNC - coal mine(s) (miniera di carbone)</summary>
    MNC,

    /// <summary>MNCR - chrome mine(s) (miniera di cromo)</summary>
    MNCR,

    /// <summary>MNCU - copper mine(s) (miniera di rame)</summary>
    MNCU,

    /// <summary>MNFE - iron mine(s) (miniera di ferro)</summary>
    MNFE,

    /// <summary>MNMT - monument (monumento commemorativo)</summary>
    MNMT,

    /// <summary>MNN - salt mine(s) (miniera di sale)</summary>
    MNN,

    /// <summary>MNQ - abandoned mine (miniera abbandonata)</summary>
    MNQ,

    /// <summary>MNQR - quarry(-ies) (cava)</summary>
    MNQR,

    /// <summary>MOLE - mole (molo massiccio)</summary>
    MOLE,

    /// <summary>MSQE - mosque (moschea)</summary>
    MSQE,

    /// <summary>MSSN - mission (missione religiosa)</summary>
    MSSN,

    /// <summary>MSSNQ - abandoned mission (missione abbandonata)</summary>
    MSSNQ,

    /// <summary>MSTY - monastery (monastero)</summary>
    MSTY,

    /// <summary>MTRO - metro station (stazione metropolitana)</summary>
    MTRO,

    /// <summary>MUS - museum (museo)</summary>
    MUS,

    /// <summary>NOV - novitiate (noviziato)</summary>
    NOV,

    /// <summary>NSY - nursery(-ies) (vivaio)</summary>
    NSY,

    /// <summary>OBPT - observation point (punto di osservazione)</summary>
    OBPT,

    /// <summary>OBS - observatory (osservatorio)</summary>
    OBS,

    /// <summary>OBSR - radio observatory (radiotelescopio)</summary>
    OBSR,

    /// <summary>OILJ - oil pipeline junction (nodo oleodotto)</summary>
    OILJ,

    /// <summary>OILQ - abandoned oil well (pozzo petrolifero abbandonato)</summary>
    OILQ,

    /// <summary>OILR - oil refinery (raffineria petrolio)</summary>
    OILR,

    /// <summary>OILT - tank farm (parco serbatoi petrolio)</summary>
    OILT,

    /// <summary>OILW - oil well (pozzo petrolifero)</summary>
    OILW,

    /// <summary>OPRA - opera house (teatro dell'opera)</summary>
    OPRA,

    /// <summary>PAL - palace (palazzo reale)</summary>
    PAL,

    /// <summary>PGDA - pagoda (pagoda)</summary>
    PGDA,

    /// <summary>PIER - pier (pontile)</summary>
    PIER,

    /// <summary>PKLT - parking lot (parcheggio)</summary>
    PKLT,

    /// <summary>PMPO - oil pumping station (stazione pompaggio petrolio)</summary>
    PMPO,

    /// <summary>PMPW - water pumping station (stazione pompaggio acqua)</summary>
    PMPW,

    /// <summary>PO - post office (ufficio postale)</summary>
    PO,

    /// <summary>PP - police post (posto di polizia)</summary>
    PP,

    /// <summary>PPQ - abandoned police post (posto polizia abbandonato)</summary>
    PPQ,

    /// <summary>PRKGT - park gate (ingresso parco)</summary>
    PRKGT,

    /// <summary>PRKHQ - park headquarters (sede amministrativa parco)</summary>
    PRKHQ,

    /// <summary>PRN - prison (prigione)</summary>
    PRN,

    /// <summary>PRNJ - reformatory (riformatorio)</summary>
    PRNJ,

    /// <summary>PRNQ - abandoned prison (prigione abbandonata)</summary>
    PRNQ,

    /// <summary>PS - power station (centrale elettrica)</summary>
    PS,

    /// <summary>PSH - hydroelectric power station (centrale idroelettrica)</summary>
    PSH,

    /// <summary>PSN - nuclear power station (centrale nucleare)</summary>
    PSN,

    /// <summary>PSTB - border post (posto di frontiera)</summary>
    PSTB,

    /// <summary>PSTC - customs post (posto doganale)</summary>
    PSTC,

    /// <summary>PSTP - patrol post (posto di pattuglia)</summary>
    PSTP,

    /// <summary>PYR - pyramid (piramide)</summary>
    PYR,

    /// <summary>PYRS - pyramids (piramidi)</summary>
    PYRS,

    /// <summary>QUAY - quay (banchina portuale)</summary>
    QUAY,

    /// <summary>RDCR - traffic circle (rotatoria)</summary>
    RDCR,

    /// <summary>RDIN - intersection (svincolo autostradale)</summary>
    RDIN,

    /// <summary>RECG - golf course (campo da golf)</summary>
    RECG,

    /// <summary>RECR - racetrack (pista da corsa)</summary>
    RECR,

    /// <summary>REST - restaurant (ristorante)</summary>
    REST,

    /// <summary>RET - store (negozio)</summary>
    RET,

    /// <summary>RHSE - resthouse (locanda di sosta)</summary>
    RHSE,

    /// <summary>RKRY - rookery (colonia di uccelli/foche)</summary>
    RKRY,

    /// <summary>RLG - religious site (sito religioso antico)</summary>
    RLG,

    /// <summary>RLGR - retreat (ritiro spirituale)</summary>
    RLGR,

    /// <summary>RNCH - ranch(es) (ranch)</summary>
    RNCH,

    /// <summary>RSD - railroad siding (binario di servizio)</summary>
    RSD,

    /// <summary>RSGNL - railroad signal (segnale ferroviario)</summary>
    RSGNL,

    /// <summary>RSRT - resort (località di villeggiatura)</summary>
    RSRT,

    /// <summary>RSTN - railroad station (stazione ferroviaria)</summary>
    RSTN,

    /// <summary>RSTNQ - abandoned railroad station (stazione ferroviaria abbandonata)</summary>
    RSTNQ,

    /// <summary>RSTP - railroad stop (fermata ferroviaria)</summary>
    RSTP,

    /// <summary>RSTPQ - abandoned railroad stop (fermata ferroviaria abbandonata)</summary>
    RSTPQ,

    /// <summary>RUIN - ruin(s) (rovine)</summary>
    RUIN,

    /// <summary>SCH - school (scuola)</summary>
    SCH,

    /// <summary>SCHA - agricultural school (scuola agricola)</summary>
    SCHA,

    /// <summary>SCHC - college (college, università)</summary>
    SCHC,

    /// <summary>SCHL - language school (scuola di lingue)</summary>
    SCHL,

    /// <summary>SCHM - military school (accademia militare)</summary>
    SCHM,

    /// <summary>SCHN - maritime school (scuola nautica)</summary>
    SCHN,

    /// <summary>SCHT - technical school (scuola tecnica)</summary>
    SCHT,

    /// <summary>SECP - State Exam Prep Centre (centro preparazione esami di stato)</summary>
    SECP,

    /// <summary>SHPF - sheepfold (ovile)</summary>
    SHPF,

    /// <summary>SHRN - shrine (santuario)</summary>
    SHRN,

    /// <summary>SHSE - storehouse (magazzino)</summary>
    SHSE,

    /// <summary>SLCE - sluice (chiusa idraulica)</summary>
    SLCE,

    /// <summary>SNTR - sanatorium (sanatorio)</summary>
    SNTR,

    /// <summary>SPA - spa (stazione termale)</summary>
    SPA,

    /// <summary>SPLY - spillway (sfioratore di diga)</summary>
    SPLY,

    /// <summary>SQR - square (piazza)</summary>
    SQR,

    /// <summary>STBL - stable (stalla)</summary>
    STBL,

    /// <summary>STDM - stadium (stadio)</summary>
    STDM,

    /// <summary>STNB - scientific research base (base di ricerca scientifica)</summary>
    STNB,

    /// <summary>STNC - coast guard station (stazione guardia costiera)</summary>
    STNC,

    /// <summary>STNE - experiment station (stazione sperimentale)</summary>
    STNE,

    /// <summary>STNF - forest station (stazione forestale)</summary>
    STNF,

    /// <summary>STNI - inspection station (stazione di ispezione)</summary>
    STNI,

    /// <summary>STNM - meteorological station (stazione meteorologica)</summary>
    STNM,

    /// <summary>STNR - radio station (stazione radio)</summary>
    STNR,

    /// <summary>STNS - satellite station (stazione satellitare)</summary>
    STNS,

    /// <summary>STNW - whaling station (stazione baleniera)</summary>
    STNW,

    /// <summary>STPS - steps (gradini, scalinata)</summary>
    STPS,

    /// <summary>SWT - sewage treatment plant (depuratore acque)</summary>
    SWT,

    /// <summary>SYG - synagogue (sinagoga)</summary>
    SYG,

    /// <summary>THTR - theater (teatro)</summary>
    THTR,

    /// <summary>TMB - tomb(s) (tomba monumentale)</summary>
    TMB,

    /// <summary>TMPL - temple(s) (tempio)</summary>
    TMPL,

    /// <summary>TNKD - cattle dipping tank (vasca disinfestazione bestiame)</summary>
    TNKD,

    /// <summary>TOLL - toll gate/barrier (casello autostradale)</summary>
    TOLL,

    /// <summary>TOWR - tower (torre)</summary>
    TOWR,

    /// <summary>TRAM - tram (tram)</summary>
    TRAM,

    /// <summary>TRANT - transit terminal (terminal trasporti)</summary>
    TRANT,

    /// <summary>TRIG - triangulation station (punto trigonometrico)</summary>
    TRIG,

    /// <summary>TRMO - oil pipeline terminal (terminal oleodotto)</summary>
    TRMO,

    /// <summary>TWO - temp work office (ufficio lavoro temporaneo)</summary>
    TWO,

    /// <summary>UNIP - university prep school (scuola preparazione universitaria)</summary>
    UNIP,

    /// <summary>UNIV - university (università)</summary>
    UNIV,

    /// <summary>USGE - united states government establishment (struttura governo USA)</summary>
    USGE,

    /// <summary>VETF - veterinary facility (struttura veterinaria)</summary>
    VETF,

    /// <summary>WALL - wall (muro)</summary>
    WALL,

    /// <summary>WALLA - ancient wall (muro antico)</summary>
    WALLA,

    /// <summary>WEIR - weir(s) (sbarramento fluviale)</summary>
    WEIR,

    /// <summary>WHRF - wharf(-ves) (banchina aperta)</summary>
    WHRF,

    /// <summary>WRCK - wreck (relitto)</summary>
    WRCK,

    /// <summary>WTRW - waterworks (acquedotto urbano)</summary>
    WTRW,

    /// <summary>ZNF - free trade zone (zona franca)</summary>
    ZNF,

    /// <summary>ZOO - zoo (zoo)</summary>
    ZOO,

    // ===== T - mountain, hill, rock (Montagne, colline, rocce) =====

    /// <summary>ASPH - asphalt lake (lago di asfalto naturale)</summary>
    ASPH,

    /// <summary>ATOL - atoll(s) (atollo)</summary>
    ATOL,

    /// <summary>BAR - bar (barra di sedimenti)</summary>
    BAR,

    /// <summary>BCH - beach (spiaggia)</summary>
    BCH,

    /// <summary>BCHS - beaches (spiagge)</summary>
    BCHS,

    /// <summary>BDLD - badlands (calanchi)</summary>
    BDLD,

    /// <summary>BLDR - boulder field (campo di massi)</summary>
    BLDR,

    /// <summary>BLHL - blowhole(s) (soffione marino)</summary>
    BLHL,

    /// <summary>BLOW - blowout(s) (depressione eolica)</summary>
    BLOW,

    /// <summary>BNCH - bench (terrazzo roccioso)</summary>
    BNCH,

    /// <summary>BUTE - butte(s) (mesa isolata)</summary>
    BUTE,

    /// <summary>CAPE - cape (capo)</summary>
    CAPE,

    /// <summary>CFT - cleft(s) (fenditura costiera)</summary>
    CFT,

    /// <summary>CLDA - caldera (caldera vulcanica)</summary>
    CLDA,

    /// <summary>CLF - cliff(s) (scogliera, falesia)</summary>
    CLF,

    /// <summary>CNYN - canyon (canyon)</summary>
    CNYN,

    /// <summary>CONE - cone(s) (cono vulcanico o di fango)</summary>
    CONE,

    /// <summary>CRDR - corridor (corridoio geografico)</summary>
    CRDR,

    /// <summary>CRQ - cirque (circo glaciale)</summary>
    CRQ,

    /// <summary>CRQS - cirques (circhi glaciali)</summary>
    CRQS,

    /// <summary>CRTR - crater(s) (cratere)</summary>
    CRTR,

    /// <summary>CUET - cuesta(s) (cuesta)</summary>
    CUET,

    /// <summary>DLTA - delta (delta fluviale)</summary>
    DLTA,

    /// <summary>DPR - depression(s) (depressione)</summary>
    DPR,

    /// <summary>DSRT - desert (deserto)</summary>
    DSRT,

    /// <summary>DUNE - dune(s) (duna)</summary>
    DUNE,

    /// <summary>DVD - divide (spartiacque)</summary>
    DVD,

    /// <summary>ERG - sandy desert (erg, deserto sabbioso)</summary>
    ERG,

    /// <summary>FAN - fan(s) (conoide alluvionale)</summary>
    FAN,

    /// <summary>FORD - ford (guado)</summary>
    FORD,

    /// <summary>FSR - fissure (fessura vulcanica)</summary>
    FSR,

    /// <summary>GAP - gap (varco in catena montuosa)</summary>
    GAP,

    /// <summary>GRGE - gorge(s) (gola)</summary>
    GRGE,

    /// <summary>HDLD - headland (promontorio)</summary>
    HDLD,

    /// <summary>HLL - hill (collina)</summary>
    HLL,

    /// <summary>HLLS - hills (colline)</summary>
    HLLS,

    /// <summary>HMCK - hammock(s) (dosso in zona umida)</summary>
    HMCK,

    /// <summary>HMDA - rock desert (hammada, deserto roccioso)</summary>
    HMDA,

    /// <summary>INTF - interfluve (interfluvio)</summary>
    INTF,

    /// <summary>ISL - island (isola)</summary>
    ISL,

    /// <summary>ISLET - islet (isoletta)</summary>
    ISLET,

    /// <summary>ISLF - artificial island (isola artificiale)</summary>
    ISLF,

    /// <summary>ISLM - mangrove island (isola di mangrovie)</summary>
    ISLM,

    /// <summary>ISLS - islands (isole)</summary>
    ISLS,

    /// <summary>ISLT - land-tied island (tombolo)</summary>
    ISLT,

    /// <summary>ISLX - section of island (sezione di isola)</summary>
    ISLX,

    /// <summary>ISTH - isthmus (istmo)</summary>
    ISTH,

    /// <summary>KRST - karst area (area carsica)</summary>
    KRST,

    /// <summary>LAVA - lava area (colata lavica)</summary>
    LAVA,

    /// <summary>LEV - levee (argine naturale)</summary>
    LEV,

    /// <summary>MESA - mesa(s) (mesa)</summary>
    MESA,

    /// <summary>MND - mound(s) (monticello)</summary>
    MND,

    /// <summary>MRN - moraine (morena glaciale)</summary>
    MRN,

    /// <summary>MT - mountain (montagna)</summary>
    MT,

    /// <summary>MTS - mountains (montagne)</summary>
    MTS,

    /// <summary>NKM - meander neck (collo di meandro)</summary>
    NKM,

    /// <summary>NTK - nunatak (nunatak)</summary>
    NTK,

    /// <summary>NTKS - nunataks (nunatak)</summary>
    NTKS,

    /// <summary>PAN - pan (depressione con lago temporaneo)</summary>
    PAN,

    /// <summary>PANS - pans (depressioni con laghi temporanei)</summary>
    PANS,

    /// <summary>PASS - pass (passo montano)</summary>
    PASS,

    /// <summary>PEN - peninsula (penisola)</summary>
    PEN,

    /// <summary>PENX - section of peninsula (sezione di penisola)</summary>
    PENX,

    /// <summary>PK - peak (vetta, picco)</summary>
    PK,

    /// <summary>PKS - peaks (vette, picchi)</summary>
    PKS,

    /// <summary>PLAT - plateau (altopiano)</summary>
    PLAT,

    /// <summary>PLATX - section of plateau (sezione di altopiano)</summary>
    PLATX,

    /// <summary>PLDR - polder (polder)</summary>
    PLDR,

    /// <summary>PLN - plain(s) (pianura)</summary>
    PLN,

    /// <summary>PLNX - section of plain (sezione di pianura)</summary>
    PLNX,

    /// <summary>PROM - promontory(-ies) (promontorio)</summary>
    PROM,

    /// <summary>PT - point (punta)</summary>
    PT,

    /// <summary>PTS - points (punte)</summary>
    PTS,

    /// <summary>RDGB - beach ridge (cordone litoraneo)</summary>
    RDGB,

    /// <summary>RDGE - ridge(s) (cresta montuosa)</summary>
    RDGE,

    /// <summary>REG - stony desert (reg, deserto pietroso)</summary>
    REG,

    /// <summary>RK - rock (roccia isolata)</summary>
    RK,

    /// <summary>RKFL - rockfall (frana rocciosa)</summary>
    RKFL,

    /// <summary>RKS - rocks (rocce isolate)</summary>
    RKS,

    /// <summary>SAND - sand area (area sabbiosa)</summary>
    SAND,

    /// <summary>SBED - dry stream bed (letto di fiume asciutto)</summary>
    SBED,

    /// <summary>SCRP - escarpment (scarpata)</summary>
    SCRP,

    /// <summary>SDL - saddle (sella montuosa)</summary>
    SDL,

    /// <summary>SHOR - shore (riva)</summary>
    SHOR,

    /// <summary>SINK - sinkhole (dolina carsica)</summary>
    SINK,

    /// <summary>SLID - slide (frana)</summary>
    SLID,

    /// <summary>SLP - slope(s) (pendio)</summary>
    SLP,

    /// <summary>SPIT - spit (freccia litoranea)</summary>
    SPIT,

    /// <summary>SPUR - spur(s) (sperone montuoso)</summary>
    SPUR,

    /// <summary>TAL - talus slope (falda di detrito)</summary>
    TAL,

    /// <summary>TRGD - interdune trough(s) (depressione tra dune)</summary>
    TRGD,

    /// <summary>TRR - terrace (terrazza alluvionale)</summary>
    TRR,

    /// <summary>UPLD - upland (altopiano interno)</summary>
    UPLD,

    /// <summary>VAL - valley (valle)</summary>
    VAL,

    /// <summary>VALG - hanging valley (valle sospesa)</summary>
    VALG,

    /// <summary>VALS - valleys (valli)</summary>
    VALS,

    /// <summary>VALX - section of valley (sezione di valle)</summary>
    VALX,

    /// <summary>VLC - volcano (vulcano)</summary>
    VLC,

    // ===== U - undersea (Sottomarini) =====

    /// <summary>APNU - apron (pendio sottomarino)</summary>
    APNU,

    /// <summary>ARCU - arch (arco sottomarino)</summary>
    ARCU,

    /// <summary>ARRU - arrugado (area corrugata sottomarina)</summary>
    ARRU,

    /// <summary>BDLU - borderland (area di confine sottomarina)</summary>
    BDLU,

    /// <summary>BKSU - banks (banchi sottomarini)</summary>
    BKSU,

    /// <summary>BNKU - bank (banco sottomarino)</summary>
    BNKU,

    /// <summary>BSNU - basin (bacino sottomarino)</summary>
    BSNU,

    /// <summary>CDAU - cordillera (cordigliera sottomarina)</summary>
    CDAU,

    /// <summary>CNSU - canyons (canyon sottomarini)</summary>
    CNSU,

    /// <summary>CNYU - canyon (canyon sottomarino)</summary>
    CNYU,

    /// <summary>CRSU - continental rise (scarpata continentale)</summary>
    CRSU,

    /// <summary>DEPU - deep (depressione profonda)</summary>
    DEPU,

    /// <summary>EDGU - shelf edge (bordo piattaforma continentale)</summary>
    EDGU,

    /// <summary>ESCU - escarpment (scarpata sottomarina)</summary>
    ESCU,

    /// <summary>FANU - fan (ventaglio sottomarino)</summary>
    FANU,

    /// <summary>FLTU - flat (pianura sottomarina)</summary>
    FLTU,

    /// <summary>FRZU - fracture zone (zona di frattura)</summary>
    FRZU,

    /// <summary>FURU - furrow (solco sottomarino)</summary>
    FURU,

    /// <summary>GAPU - gap (varco sottomarino)</summary>
    GAPU,

    /// <summary>GLYU - gully (canalone sottomarino)</summary>
    GLYU,

    /// <summary>HLLU - hill (collina sottomarina)</summary>
    HLLU,

    /// <summary>HLSU - hills (colline sottomarine)</summary>
    HLSU,

    /// <summary>HOLU - hole (buca sottomarina)</summary>
    HOLU,

    /// <summary>KNLU - knoll (rilievo sottomarino)</summary>
    KNLU,

    /// <summary>KNSU - knolls (rilievi sottomarini)</summary>
    KNSU,

    /// <summary>LDGU - ledge (sporgenza rocciosa sottomarina)</summary>
    LDGU,

    /// <summary>LEVU - levee (argine sottomarino)</summary>
    LEVU,

    /// <summary>MESU - mesa (mesa sottomarina)</summary>
    MESU,

    /// <summary>MNDU - mound (montarozzo sottomarino)</summary>
    MNDU,

    /// <summary>MOTU - moat (fossato sottomarino)</summary>
    MOTU,

    /// <summary>MTU - mountain (montagna sottomarina)</summary>
    MTU,

    /// <summary>PKSU - peaks (picchi sottomarini)</summary>
    PKSU,

    /// <summary>PKU - peak (picco sottomarino)</summary>
    PKU,

    /// <summary>PLNU - plain (pianura sottomarina)</summary>
    PLNU,

    /// <summary>PLTU - plateau (altopiano sottomarino)</summary>
    PLTU,

    /// <summary>PNLU - pinnacle (pinnacolo sottomarino)</summary>
    PNLU,

    /// <summary>PRVU - province (provincia sottomarina)</summary>
    PRVU,

    /// <summary>RDGU - ridge (dorsale sottomarina)</summary>
    RDGU,

    /// <summary>RDSU - ridges (dorsali sottomarine)</summary>
    RDSU,

    /// <summary>RFSU - reefs (scogliere sottomarine)</summary>
    RFSU,

    /// <summary>RFU - reef (scogliera sottomarina)</summary>
    RFU,

    /// <summary>RISU - rise (elevazione sottomarina)</summary>
    RISU,

    /// <summary>SCNU - seachannel (canale sottomarino)</summary>
    SCNU,

    /// <summary>SCSU - seachannels (canali sottomarini)</summary>
    SCSU,

    /// <summary>SDLU - saddle (sella sottomarina)</summary>
    SDLU,

    /// <summary>SHFU - shelf (piattaforma continentale)</summary>
    SHFU,

    /// <summary>SHLU - shoal (bassofondo)</summary>
    SHLU,

    /// <summary>SHSU - shoals (bassifondi)</summary>
    SHSU,

    /// <summary>SHVU - shelf valley (valle di piattaforma)</summary>
    SHVU,

    /// <summary>SILU - sill (soglia sottomarina)</summary>
    SILU,

    /// <summary>SLPU - slope (pendio continentale)</summary>
    SLPU,

    /// <summary>SMSU - seamounts (montagne sottomarine)</summary>
    SMSU,

    /// <summary>SMU - seamount (montagna sottomarina)</summary>
    SMU,

    /// <summary>SPRU - spur (sperone sottomarino)</summary>
    SPRU,

    /// <summary>TERU - terrace (terrazza sottomarina)</summary>
    TERU,

    /// <summary>TMSU - tablemounts (guyot)</summary>
    TMSU,

    /// <summary>TMTU - tablemount (guyot singolo)</summary>
    TMTU,

    /// <summary>TNGU - tongue (lingua sottomarina)</summary>
    TNGU,

    /// <summary>TRGU - trough (depressione sottomarina)</summary>
    TRGU,

    /// <summary>TRNU - trench (fossa oceanica)</summary>
    TRNU,

    /// <summary>VALU - valley (valle sottomarina)</summary>
    VALU,

    /// <summary>VLSU - valleys (valli sottomarine)</summary>
    VLSU,

    // ===== V - forest, heath (Foreste, vegetazione) =====

    /// <summary>BUSH - bush(es) (cespugli isolati)</summary>
    BUSH,

    /// <summary>CULT - cultivated area (area coltivata)</summary>
    CULT,

    /// <summary>FRST - forest(s) (foresta)</summary>
    FRST,

    /// <summary>FRSTF - fossilized forest (foresta fossile)</summary>
    FRSTF,

    /// <summary>GROVE - grove (boschetto)</summary>
    GROVE,

    /// <summary>GRSLD - grassland (prateria)</summary>
    GRSLD,

    /// <summary>GRVC - coconut grove (piantagione di cocco)</summary>
    GRVC,

    /// <summary>GRVO - olive grove (oliveto)</summary>
    GRVO,

    /// <summary>GRVP - palm grove (palmeto)</summary>
    GRVP,

    /// <summary>GRVPN - pine grove (pineta)</summary>
    GRVPN,

    /// <summary>HTH - heath (brughiera)</summary>
    HTH,

    /// <summary>MDW - meadow (prato umido)</summary>
    MDW,

    /// <summary>OCH - orchard(s) (frutteto)</summary>
    OCH,

    /// <summary>SCRB - scrubland (macchia arbustiva)</summary>
    SCRB,

    /// <summary>TREE - tree(s) (albero isolato)</summary>
    TREE,

    /// <summary>TUND - tundra (tundra)</summary>
    TUND,

    /// <summary>VIN - vineyard (vigneto)</summary>
    VIN,

    /// <summary>VINS - vineyards (vigneti)</summary>
    VINS,
}
