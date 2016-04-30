var setIconToTrailType = function (type) {
    var res = '';
    switch (type) {
        case 'oneday': res = 'one-day.png'
            break;
        case 'manydays': res = 'many-days.png'
            break;
        case 'weekend': res = 'weekend.png'
            break;
    }
    return res
}

var setIconToTrailDurationType = function (durationtype) {
    var res = '';
    switch (durationtype) {
        case 'loop': res = 'loop.png'
            break;
        case 'point-to-point': res = 'point-to-point.png'
            break;
        case 'in-and-out': res = 'in-and-out.png'
            break;
    }
    return res
}

var setTextTrailType = function (type) {
    var res = '';
    switch (type) {
        case 'oneday': res = 'One day'
            break;
        case 'manydays': res = 'Many days'
            break;
        case 'weekend': res = 'Weekend'
            break;
    }
    return res
}

var setTextTrailDurationType = function (durationtype) {
    var res = '';
    switch (durationtype) {
        case 'loop': res = 'Loop'
            break;
        case 'point-to-point': res = 'Point to point'
            break;
        case 'in-and-out': res = 'In and Out'
            break;
    }
    return res
}

var setLabelClassForDifficult = function (diff) {
    var res = "label label-default";

    switch (diff) {
        case "easy": res = "label label-success";
            break;
        case "medium": res = "label label-warning";
            break;
        case "hard": res = "label label-danger";
            break;
    }

    return res;
}