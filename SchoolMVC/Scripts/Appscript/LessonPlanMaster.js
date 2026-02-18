//var typingBox = document.getElementById('LP_LessonContent');
//var langSelect = document.getElementById('lang-select');
//var menu = document.getElementById('suggestion-menu');
//var lastEnglishWord = "";

//// 1. Convert on Space
//typingBox.addEventListener('keydown', function (e) {
//    var lang = langSelect.value;
//    if (lang === 'en') return;

//    if (e.keyCode === 32) {
//        var text = typingBox.value;
//        var words = text.trim().split(/\s+/);
//        var currentWord = words[words.length - 1];

//        if (currentWord) {
//            lastEnglishWord = currentWord;
//            fetchSuggestions(currentWord, lang, function (suggestions) {
//                if (suggestions && suggestions.length > 0) {
//                    replaceLastWord(suggestions[0]);
//                }
//            });
//        }
//    }
//});

//// 2. Show Menu on Backspace
//typingBox.addEventListener('keyup', function (e) {
//    var lang = langSelect.value;
//    if (lang === 'en' || !lastEnglishWord) return;

//    if (e.keyCode === 8) {
//        fetchSuggestions(lastEnglishWord, lang, function (suggestions) {
//            if (suggestions && suggestions.length > 0) {
//                showMenu(suggestions);
//            }
//        });
//    }
//});

//function fetchSuggestions(word, lang, callback) {
//    var url = "https://inputtools.google.com/request?text=" + encodeURIComponent(word) + "&itc=" + lang + "&num=8&cp=0&cs=1&ie=utf-8&oe=utf-8&app=demopage";

//    var xhr = new XMLHttpRequest();
//    xhr.open("GET", url, true);
//    xhr.onreadystatechange = function () {
//        if (xhr.readyState === 4 && xhr.status === 200) {
//            try {
//                var data = JSON.parse(xhr.responseText);
//                if (data[0] === 'SUCCESS') {
//                    callback(data[1][0][1]);
//                }
//            } catch (err) { }
//        }
//    };
//    xhr.send();
//}

//function showMenu(list) {
//    menu.innerHTML = "";
//    // FORCE STYLES via JS to override any CSS files
//    menu.style.cssText = "display: block !important; position: fixed !important; " +
//                         "background: #fff !important; border: 2px solid #333 !important; " +
//                         "padding: 10px !important; z-index: 999999 !important; " +
//                         "list-style: none !important; min-width: 150px !important; " +
//                         "box-shadow: 5px 5px 15px rgba(0,0,0,0.5) !important;";

//    // Position it in the center of the screen initially to verify it works
//    menu.style.top = "20%";
//    menu.style.left = "40%";

//    for (var i = 0; i < list.length; i++) {
//        var li = document.createElement('li');
//        li.style.cssText = "padding: 10px; cursor: pointer; border-bottom: 1px solid #ccc; color: #000; font-weight: bold;";
//        li.innerText = list[i];

//        // Use a closure to capture the word correctly
//        (function (w) {
//            li.onclick = function () {
//                replaceLastWord(w);
//                hideMenu();
//            };
//        })(list[i]);

//        menu.appendChild(li);
//    }
//}

//function replaceLastWord(newWord) {
//    var text = typingBox.value;
//    var words = text.trim().split(/\s+/);
//    if (words.length > 0) {
//        words[words.length - 1] = newWord;
//        typingBox.value = words.join(' ') + ' ';
//    }
//    typingBox.focus();
//}

//function hideMenu() {
//    menu.style.display = 'none';
//}

//// Close menu if clicking anywhere else
//document.addEventListener('mousedown', function (e) {
//    if (e.target !== menu && !menu.contains(e.target)) {
//        hideMenu();
//    }
//});


//document.addEventListener("DOMContentLoaded", function () {
//    var lastEnglishWord = "";
//    var currentTypingBox = null;
//    var currentLangSelect = null;
//    var currentMenu = null;

//    // This "Event Delegation" handles ALL textboxes automatically when you click/tab into them
//    document.addEventListener('focusin', function (e) {
//        // Check if the focused element is a transliterate box or text-editor
//        if (e.target && (e.target.classList.contains('transliterate-box') || e.target.classList.contains('text-editor'))) {

//            // 1. Identify the active elements for THIS specific box
//            currentTypingBox = e.target;
//            var container = currentTypingBox.closest('.col-lg-4');
//            currentLangSelect = container.querySelector('select');
//            currentMenu = container.querySelector('#suggestion-menu');

//            // 2. Attach the KeyDown Logic (Conversion on Space)
//            currentTypingBox.onkeydown = function (event) {
//                var lang = currentLangSelect.value;
//                if (lang === 'en') return;

//                if (event.keyCode === 32) { // Space key
//                    var text = currentTypingBox.value;
//                    var words = text.trim().split(/\s+/);
//                    var currentWord = words[words.length - 1];

//                    if (currentWord) {
//                        lastEnglishWord = currentWord;
//                        fetchSuggestions(currentWord, lang, function (suggestions) {
//                            if (suggestions && suggestions.length > 0) {
//                                replaceLastWord(currentTypingBox, suggestions[0]);
//                            }
//                        });
//                    }
//                }
//            };

//            // 3. Attach the KeyUp Logic (Alternatives on Backspace)
//            currentTypingBox.onkeyup = function (event) {
//                var lang = currentLangSelect.value;
//                if (lang === 'en' || !lastEnglishWord) return;

//                if (event.keyCode === 8) { // Backspace
//                    fetchSuggestions(lastEnglishWord, lang, function (suggestions) {
//                        if (suggestions && suggestions.length > 0) {
//                            showMenu(currentMenu, suggestions, currentTypingBox);
//                        }
//                    });
//                }
//            };
//        }
//    });

//    function fetchSuggestions(word, lang, callback) {
//        var url = "https://inputtools.google.com/request?text=" + encodeURIComponent(word) + "&itc=" + lang + "&num=8&cp=0&cs=1&ie=utf-8&oe=utf-8&app=demopage";
//        var xhr = new XMLHttpRequest();
//        xhr.open("GET", url, true);
//        xhr.onreadystatechange = function () {
//            if (xhr.readyState === 4 && xhr.status === 200) {
//                try {
//                    var data = JSON.parse(xhr.responseText);
//                    if (data[0] === 'SUCCESS') {
//                        callback(data[1][0][1]);
//                    }
//                } catch (err) { console.error("API Error"); }
//            }
//        };
//        xhr.send();
//    }

//    function showMenu(menu, list, activeBox) {
//        if (!menu) return;
//        menu.innerHTML = "";

//        // Force styling to ensure it appears correctly relative to the box
//        menu.style.cssText = "display: block !important; position: absolute !important; " +
//                             "background: #fff !important; border: 2px solid #333 !important; " +
//                             "padding: 5px !important; z-index: 9999 !important; " +
//                             "list-style: none !important; min-width: 150px !important; " +
//                             "box-shadow: 5px 5px 15px rgba(0,0,0,0.3) !important; margin: 0;";

//        for (var i = 0; i < list.length; i++) {
//            var li = document.createElement('li');
//            li.style.cssText = "padding: 8px; cursor: pointer; border-bottom: 1px solid #eee; color: #000; font-weight: bold;";
//            li.innerText = list[i];

//            (function (word) {
//                li.onclick = function () {
//                    replaceLastWord(activeBox, word);
//                    menu.style.display = 'none';
//                };
//            })(list[i]);

//            menu.appendChild(li);
//        }
//    }

//    function replaceLastWord(box, newWord) {
//        var text = box.value;
//        var words = text.split(/\s+/);
//        if (words.length > 0) {
//            words[words.length - 1] = newWord;
//            box.value = words.join(' ') + ' ';
//        }
//        box.focus();
//    }

//    // Global click listener to hide any open menu
//    document.addEventListener('mousedown', function (e) {
//        var openMenus = document.querySelectorAll('#suggestion-menu');
//        openMenus.forEach(function (m) {
//            if (e.target !== m && !m.contains(e.target)) {
//                m.style.display = 'none';
//            }
//        });
//    });
//});

document.addEventListener("DOMContentLoaded", function () {
    var lastEnglishWord = "";
    var currentMenu = null;

    // Use Event Delegation to catch focus on any textarea
    document.addEventListener('focusin', function (e) {
        if (e.target && e.target.classList.contains('form-control')) {
            var typingBox = e.target;
            var container = typingBox.closest('.col-lg-4');
            if (!container) return;

            var langSelect = container.querySelector('select');
            currentMenu = container.querySelector('#suggestion-menu');

            // 1. Conversion on Space (The fix for "both words staying" is here)
            typingBox.onkeydown = function (event) {
                var lang = langSelect.value;
                if (lang === 'en') return;

                if (event.keyCode === 32) { // Space
                    var text = typingBox.value;
                    // Get word immediately before the cursor
                    var words = text.split(/\s+/);
                    var currentWord = words[words.length - 1];

                    if (currentWord && currentWord.length > 0) {
                        lastEnglishWord = currentWord;
                        // We prevent default space for a split second to handle replacement
                        fetchSuggestions(currentWord, lang, function (suggestions) {
                            if (suggestions && suggestions.length > 0) {
                                replaceLastWord(typingBox, suggestions[0]);
                            }
                        });
                    }
                }
            };

            // 2. Show Alternatives on Backspace
            typingBox.onkeyup = function (event) {
                var lang = langSelect.value;
                if (lang === 'en' || !lastEnglishWord) return;

                if (event.keyCode === 8) { // Backspace
                    fetchSuggestions(lastEnglishWord, lang, function (suggestions) {
                        if (suggestions && suggestions.length > 0) {
                            showMenu(currentMenu, suggestions, typingBox);
                        }
                    });
                }
            };
        }
    });

    function fetchSuggestions(word, lang, callback) {
        var url = "https://inputtools.google.com/request?text=" + encodeURIComponent(word) + "&itc=" + lang + "&num=8&cp=0&cs=1&ie=utf-8&oe=utf-8&app=demopage";
        var xhr = new XMLHttpRequest();
        xhr.open("GET", url, true);
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4 && xhr.status === 200) {
                try {
                    var data = JSON.parse(xhr.responseText);
                    if (data[0] === 'SUCCESS') callback(data[1][0][1]);
                } catch (err) { }
            }
        };
        xhr.send();
    }

    function showMenu(menu, list, activeBox) {
        if (!menu) return;
        menu.innerHTML = "";
        menu.style.cssText = "display: block !important; position: absolute !important; background: #fff !important; border: 1px solid #333 !important; z-index: 9999 !important; list-style: none !important; padding: 0 !important; margin: 0 !important; box-shadow: 2px 2px 10px rgba(0,0,0,0.2) !important;";

        list.forEach(function (word) {
            var li = document.createElement('li');
            li.style.cssText = "padding: 8px 12px; cursor: pointer; border-bottom: 1px solid #eee; color: #000; font-size: 14px;";
            li.innerText = word;
            li.onclick = function () {
                replaceLastWord(activeBox, word);
                menu.style.display = 'none';
            };
            menu.appendChild(li);
        });
    }

    // THE FIX: This function now properly removes the English word before adding the Transliterated one
    function replaceLastWord(box, newWord) {
        var text = box.value.trim();
        var lastSpaceIndex = text.lastIndexOf(" ");

        if (lastSpaceIndex === -1) {
            // Only one word exists
            box.value = newWord + " ";
        } else {
            // Keep everything UP TO the last space, then add the new word
            box.value = text.substring(0, lastSpaceIndex) + " " + newWord + " ";
        }
        box.focus();
    }

    document.addEventListener('mousedown', function (e) {
        var menus = document.querySelectorAll('#suggestion-menu');
        menus.forEach(function (m) {
            if (e.target !== m && !m.contains(e.target)) m.style.display = 'none';
        });
    });
});
    // ------------------------------
    // 1) Label dictionaries (BN/EN)
    // ------------------------------
//    const LABELS = {
//        bn: {
//            objectives: "উদ্দেশ্য",
//            materials_required: "প্রয়োজনীয় উপকরণ",
//            introduction: "ভূমিকা",
//            time_breakdown: "সময় বিভাজন",
//            teaching_steps: "পাঠদানের ধাপ",
//            activities: "কার্যক্রম",
//            practice_work: "অনুশীলন",
//            assessment: "মূল্যায়ন",
//            homework: "বাড়ির কাজ",
//            expected_learning_outcome: "প্রত্যাশিত শেখার ফলাফল"
//        },
//        en: {
//            objectives: "Objectives",
//            materials_required: "Materials Required",
//            introduction: "Introduction",
//            time_breakdown: "Time Breakdown",
//            teaching_steps: "Teaching Steps",
//            activities: "Activities",
//            practice_work: "Practice Work",
//            assessment: "Assessment",
//            homework: "Homework",
//            expected_learning_outcome: "Expected Learning Outcome"
//        }
//    };

//// Fixed property order (keys fixed, values dynamic)
//const SECTION_ORDER = [
//    "objectives",
//    "materials_required",
//    "introduction",
//    "time_breakdown",
//    "teaching_steps",
//    "activities",
//    "practice_work",
//    "assessment",
//    "homework",
//    "expected_learning_outcome"
//];

//// ------------------------------
//// 2) Helpers
//// ------------------------------
//function escapeHtml(v) {
//    if (v === null || v === undefined) return "";
//    return String(v)
//        .replace(/&/g, "&amp;")
//        .replace(/</g, "&lt;")
//        .replace(/>/g, "&gt;")
//        .replace(/"/g, "&quot;")
//        .replace(/'/g, "&#039;");
//}

//function isPlainObject(x) {
//    return x && typeof x === "object" && !Array.isArray(x);
//}

//function isArrayOfStrings(arr) {
//    return Array.isArray(arr) && arr.length > 0 && arr.every(function (x) { return typeof x === "string"; });
//}

//function isArrayOfObjects(arr) {
//    return Array.isArray(arr) && arr.length > 0 && arr.every(function (x) { return isPlainObject(x); });
//}

//// Auto language detection: if Bengali Unicode present → bn else en
//function detectLang(text) {
//    // Bengali range: \u0980-\u09FF
//    if (!text) return "en";
//    return /[\u0980-\u09FF]/.test(text) ? "bn" : "en";
//}

//// ------------------------------
//// 3) Renderers (dynamic data safe)
//// ------------------------------
//function renderBulletList(arr) {
//    if (!arr || !arr.length) return "";
//    var li = arr.map(function (x) { return "<li>" + escapeHtml(x) + "</li>"; }).join("");
//    return "<ul class='lp-ul'>" + li + "</ul>";
//}

//function renderTableFromObjectArray(arr, lang, labels) {
//    if (!arr || !arr.length) return "";

//    // dynamic columns (in case future object has extra keys)
//    var cols = [];
//    arr.forEach(function (o) {
//        Object.keys(o || {}).forEach(function (k) {
//            if (cols.indexOf(k) === -1) cols.push(k);
//        });
//    });

//    // Optional: for known keys you may want translated headers too
//    // Example for time_breakdown activity/minutes:
//    var headerMap = (lang === "bn")
//        ? { activity: "কার্যক্রম", minutes: "মিনিট" }
//        : { activity: "Activity", minutes: "Minutes" };

//    var thead = cols.map(function (c) {
//        return "<th>" + escapeHtml(headerMap[c] || c) + "</th>";
//    }).join("");

//    var tbody = arr.map(function (row) {
//        var tds = cols.map(function (c) {
//            return "<td>" + escapeHtml(row ? row[c] : "") + "</td>";
//        }).join("");
//        return "<tr>" + tds + "</tr>";
//    }).join("");

//    return "<table class='lp-table'><thead><tr>" + thead + "</tr></thead><tbody>" + tbody + "</tbody></table>";
//}

//// Render any value:
//// - string/number/bool => <p>
//// - array string => <ul>
//// - array object => table
//// - object => key/value blocks
//function renderSectionValue(val, lang) {
//    if (val === null || val === undefined) return "";
//    if (typeof val === "string") {
//        var s = val.trim();
//        return s ? ("<p class='lp-p'>" + escapeHtml(s) + "</p>") : "";
//    }
//    if (typeof val === "number" || typeof val === "boolean") {
//        return "<p class='lp-p'>" + escapeHtml(val) + "</p>";
//    }

//    if (Array.isArray(val)) {
//        if (val.length === 0) return "";

//        if (isArrayOfStrings(val)) return renderBulletList(val);
//        if (isArrayOfObjects(val)) return renderTableFromObjectArray(val, lang);

//        // Mixed array fallback
//        return val.map(function (x) {
//            return "<div>" + renderSectionValue(x, lang) + "</div>";
//        }).join("");
//    }

//    if (isPlainObject(val)) {
//        var html = "";
//        Object.keys(val).forEach(function (k) {
//            var inner = renderSectionValue(val[k], lang);
//            if (!inner) return;
//            html += "<div style='margin:6px 0;'><b>" + escapeHtml(k) + ":</b> " + inner + "</div>";
//        });
//        return html;
//    }

//    return "";
//}

//// ------------------------------
//// 4) Main Lesson Plan HTML builder
//// ------------------------------
//function buildLessonPlanHtml(lp, payload) {

//    // language choice: based on Subject / Chapter (you can change to any rule)
//    var lang = detectLang(payload.Subject) === "bn" || detectLang(payload.Chapter) === "bn" ? "bn" : "en";
//    var labels = LABELS[lang];

//    // Header text (backend title etc sometimes null; use payload)
//    var title = (lp && lp.title && String(lp.title).trim())
//        ? lp.title
//        : (lang === "bn" ? ("লেসন প্ল্যান: " + payload.Chapter) : ("Lesson Plan: " + payload.Chapter));

//    var meta = (lang === "bn")
//        ? ("ক্লাস: " + payload.Class + " | বিষয়: " + payload.Subject + " | সময়: " + payload.DurationMinutes + " মিনিট")
//        : ("Class: " + payload.Class + " | Subject: " + payload.Subject + " | Duration: " + payload.DurationMinutes + " minutes");

//    var html = "<div class='lp-wrap'>";
//    html += "<h3 class='lp-title'>" + escapeHtml(title) + "</h3>";
//    html += "<div class='lp-meta'>" + escapeHtml(meta) + "</div>";
//    html += "<hr class='lp-hr' />";

//    // Render fixed sections, values are dynamic
//    SECTION_ORDER.forEach(function (key) {
//        var value = lp ? lp[key] : null;
//        var sectionBody = renderSectionValue(value, lang);

//        // if empty skip
//        if (!sectionBody) return;

//        html += "<div class='lp-section'>";
//        html += "<h4 class='lp-h4'>" + escapeHtml(labels[key] || key) + "</h4>";
//        html += sectionBody;
//        html += "</div>";
//    });

//    html += "</div>";
//    return html;
//}
//var noticeEditor;
//document.addEventListener('DOMContentLoaded', function () {
//    ClassicEditor
//        .create(document.querySelector('#LP_Outcome'), {
//            toolbar: [
//                'undo', 'redo', '|',
//                'bold', 'italic', 'underline', '|',
//                'bulletedList', 'numberedList', '|',
//                'link', 'blockQuote', 'insertTable'
//            ]
//        })
//        .then(function (editor) {
//            noticeEditor = editor;
//            editor.editing.view.focus();

//            // Ensure that editor is initialized before calling setData
//            // Now you can set the content here, after editor is ready
//            if (noticeEditor) {
//                // Example of dynamic HTML content to be inserted into the editor
//                var html = '<h2>Lesson Plan</h2><p>This is a dynamic content.</p>'; // Use your dynamic HTML here
//                noticeEditor.setData(html);
//            }

//        })
//        .catch(function (error) {
//            console.error('CKEditor init failed:', error);
//        });
//});

//// Prevent global handlers from blocking backspace
//document.addEventListener('keydown', function (e) {
//    if (e.key === 'Backspace' && e.target.tagName !== 'INPUT' && e.target.tagName !== 'TEXTAREA') {
//        e.stopPropagation();
//    }
//}, true);
//// ------------------------------
//// 5) Your AJAX function (final)
//// ------------------------------
//function generateLessonPlan() {
//    // Example payload (you can bind from UI)
//    var payload = {
//        Class: 1,
//        Subject: "English",
//        Chapter: "Verb",
//        DurationMinutes: 40
//    };

//    $.ajax({
//        url: rootDir + "JQuery/GenerateLessonPlan",
//        type: 'POST',
//        data: JSON.stringify(payload),
//        contentType: 'application/json; charset=utf-8',
//        success: function (res) {
//            if (res.success) {
//                // res.data is your lesson plan object
//                var html = buildLessonPlanHtml(res.data, payload);
//                //if (CKEDITOR.instances.editor) {
//                //    CKEDITOR.instances.editor.setData(html);
//                //}

//                alert(html);
//                //$("#LP_Outcome").html(html);

//                if (noticeEditor) {
//                    noticeEditor.setData(html); // Dynamically set content into CKEditor
//                } else {
//                    console.error("CKEditor instance not initialized yet.");
//                }
//            } else {
//                alert(res.message || "Failed");
//                console.log(res.details || res.raw);
//                $("#LP_Outcome").html("<div class='text-muted'>No data</div>");
//            }
//        },
//        error: function (xhr) {
//            alert("Server error");
//            console.log(xhr.responseText);
//            $("#LP_Outcome").html("<div class='text-muted'>Server error</div>");
//        }
//    });
//}
//Latest Populate
//function populateLessonPlan(responseData) {
//    // Check if the response data exists
//    if (responseData) {
//        // For 'Objectives' data, populate the corresponding textarea (without language check)
//        if (responseData.objectives && Array.isArray(responseData.objectives)) {
//            document.getElementById('LP_LearningObjectives').value = responseData.objectives.join('\n');
//        }

//        // For 'Materials Required' data, populate the corresponding textarea (without language check)
//        if (responseData.materials_required && Array.isArray(responseData.materials_required)) {
//            document.getElementById('LP_TeachingAidId').value = responseData.materials_required.join('\n');
//        }

//        // Example for 'Introduction' field
//        if (responseData.introduction) {
//            document.getElementById('LP_Introduction').value = responseData.introduction.join('\n');
//        }

//        // Example for 'Teaching Steps' field
//        if (responseData.teaching_steps) {
//            document.getElementById('LP_TeachingSteps').value = responseData.teaching_steps;
//        }

//        // You can add more fields here based on your response JSON structure
//        // If you have more sections in the response, you can follow the same method:
//        if (responseData.activities) {
//            document.getElementById('LP_Activities').value = responseData.activities;
//        }

//        if (responseData.practice_work) {
//            document.getElementById('LP_PracticeWork').value = responseData.practice_work;
//        }

//        if (responseData.assessment) {
//            document.getElementById('LP_Assesment').value = responseData.assessment;
//        }

//        if (responseData.homework) {
//            document.getElementById('LP_HomeWork').value = responseData.homework;
//        }

//        //if (responseData.learning_outcome) {
//        //    document.getElementById('LP_LearningOutcomes').value = responseData.learning_outcome;
//        //}

//    } else {
//        console.error('Invalid response data');
//    }
//}

//// Example of calling this function after receiving the response
//function generateLessonPlan() {
//    // Example payload (you can bind from UI)
//    var payload = {
//        Class: 1,
//        Subject: "English",
//        Chapter: "Verb",
//        DurationMinutes: 40
//    };

//    $.ajax({
//        url: rootDir + "JQuery/GenerateLessonPlan",
//        type: 'POST',
//        data: JSON.stringify(payload),
//        contentType: 'application/json; charset=utf-8',
//        success: function (res) {
//            if (res.success) {
//               // alert("Full JSON Response:\n\n" + JSON.stringify(res.data, null, 4));
//                document.getElementById('LP_LearningOutcomes').value = JSON.stringify(res.data, null, 4);
//                // Call the function to populate the lesson plan form with response data
//                populateLessonPlan(res.data);
//            } else {
//                alert(res.message || "Failed");
//            }
//        },
//        error: function (xhr) {
//            alert("Server error");
//            console.log(xhr.responseText);
//        }
//    });
//}
function populateLessonPlan(data) {
    // Check if the data exists
    if (data) {
        // Populate the fields with corresponding JSON values
        
        // Lesson Content (Introduction)
        document.querySelector("#LP_Introduction").value = data.introduction || '';

        // Objectives
        document.querySelector("#LP_LearningObjectives").value = data.objectives.join('\n') || '';

        // Teaching Steps
        document.querySelector("#LP_TeachingSteps").value = data.teaching_steps.join('\n') || '';

        // Activities
        document.querySelector("#LP_Activities").value = data.activities.join('\n') || '';

        // Practice Work
        document.querySelector("#LP_PracticeWork").value = data.practice_work.join('\n') || '';

        // Assessment
        document.querySelector("#LP_Assesment").value = data.assessment.join('\n') || '';

        // Homework
        document.querySelector("#LP_HomeWork").value = data.homework.join('\n') || '';

        // Expected Learning Outcome
        document.querySelector("#LP_LearningOutcomes").value = data.expected_learning_outcome || '';
        
        // Time Breakdown - Handle as text, assuming it's just an array of activities
    //    const timeBreakdown = data.time_breakdown.map(item => `${item.activity}: ${item.minutes} mins`).join('\n');
    //document.querySelector("#LP_TimeBreakdown").value = timeBreakdown || '';
        // Loop through the data and create the formatted string
        var timeBreakdown = "";
        $.each(data.time_breakdown, function (index, item) {
            timeBreakdown += item.activity + ": " + item.minutes + " mins\n";
        });

        // Set the value of the textarea
        $("#LP_TimeBreakDown").val(timeBreakdown || '');

    // Materials Required
        document.querySelector("#LP_TeachingAidId").value = data.materials_required.join('\n') || '';

    // Title, Class, Subject, Chapter, and Duration - If available
    document.querySelector("#LP_Title").value = data.title || '';
    document.querySelector("#LP_Class").value = data.Lclass || '';
    document.querySelector("#LP_Subject").value = data.subject || '';
    document.querySelector("#LP_Chapter").value = data.chapter || '';
    document.querySelector("#LP_Duration").value = data.duration_minutes || 0;
}
}
function generateLessonPlan() {
    var selectedOption = $('#ddlLP_SubjectId option:selected');
    var selectedDataCode = selectedOption.attr('subject-code');
    //alert("Selected Subject's data-code: " + selectedDataCode);
    // Call the populate function once you receive the response (assuming res.data is the full JSON response)
    $.ajax({

       
        url: rootDir + "JQuery/GenerateLessonPlan",
        type: 'POST',
        data: JSON.stringify({
            ClassName: $('#LP_ClassId option:selected').attr('data-code'), // Get class from the dropdown
            Subject: selectedDataCode, // Get subject from the text box or dropdown
            Chapter: $("#LP_LessonContent").val(), // Get chapter from the text box
            DurationMinutes: $("#LP_Duration").val(), // Get duration from the input field
            Language: $("#lang-select").val() // Get the language from the dropdown
        }),
        contentType: 'application/json; charset=utf-8',
        success: function (res) {
            if (res.success) {
                // Display the full JSON for debugging
                alert("Full JSON Response:\n\n" + JSON.stringify(res.data, null, 4));

                // Populate the text areas with the JSON data
                populateLessonPlan(res.data);
            } else {
                alert(res.message || "Failed");
            }
        },
        error: function (xhr) {
            alert("Server error");
            console.log(xhr.responseText);
        }
    });
}

function InsertUpdateLessonPlan() {

    if (!ValidateOperation()) {
        return false;
    }
    var facultyId = getEffectiveFacultyId();
    if (facultyId <= 0) {
        WarningToast("Faculty not selected.");
        return false;
    }

    var fromDate = convertToSqlDate($('#LP_FromDate').val());
    var toDate = convertToSqlDate($('#LP_ToDate').val());

    if (!fromDate || !toDate) {
        WarningToast("Select Start Date & End Date.");
        return false;
    }

    var classIds = $("#LP_ClassId").val() || [];
    var sectionIds = $("#ddlLP_SectionId").val() || [];
    var subjectId = $("#ddlLP_SubjectId").val();

    if (classIds.length === 0) {
        WarningToast("Select at least one Class.");
        return false;
    }
    if (!subjectId || subjectId == "0") {
        WarningToast("Select Subject.");
        return false;
    }

    var teachingAidIds = $("#LP_TeachingAidId").val() || [];
    var classSectionMap = "";

    classIds.forEach(function (cls) {
        sectionIds.forEach(function (sec) {
            classSectionMap += cls + "|" + sec + ",";
        });
    });

    classSectionMap = classSectionMap.replace(/,$/, "");
    $("#LP_ClassSectionMap").val(classSectionMap);
    var _data = JSON.stringify({
        LP_Id: $('#LP_Id').val() || 0,
        LP_FacultyID: facultyId,
        LP_FromDate: fromDate,
        LP_ToDate: toDate,
        LP_ClassId: classIds.join(','),
        LP_SectionId: sectionIds.join(','),
        LP_TeachingAidId: teachingAidIds.join(','),

        LP_ClassSectionMap: classSectionMap, 

        LP_SubjectId: parseInt(subjectId) || 0,
        LP_NumberOfPeriods: $("#LP_NumberOfPeriods").val(),

        LP_LessonContent: $("#LP_LessonContent").val(),
        LP_LearningObjectives: $("#LP_LearningObjectives").val(),
        LP_LearningOutcomes: $("#LP_LearningOutcomes").val(),

        LP_TypeOfOutcome: $("#LP_TypeOfOutcome").val(),
        LP_Outcome: $("#LP_Outcome").val(),

        LP_CreatedBy: $('#hdnUserid').val()
    });

    console.log("SEND = ", _data);

    $.ajax({
        url: rootDir + "JQuery/InsertUpdateLessonPlan",
        type: "POST",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (resp) {

            if (resp && resp.IsSuccess) {
                SuccessToast(resp.Message);
                $("#btnSave").attr("disabled", "disabled");

                setTimeout(function () {
                    window.location.href = rootDir + "Masters/LessonPlanList";
                }, 1500);
            }
            else {
                ErrorToast(resp.Message || "Error occurred.");
            }
        },

        error: function (xhr) {
            console.log(xhr.responseText);
            ErrorToast("Something went wrong.");
        }
    });
}

function ValidateOperation() {

    if ($('#LP_ClassId').val() == "") {
        WarningToast('Select class.');
        return false;
    }
    return true;
}
function Confirm(id) {
    if (confirm("Are you Sure?")) {
        Deletedata(id);
    }
}
function BindDropDownListForSection(ddl, dataCollection) {
    $(ddl).empty();
    $(ddl).append(new Option("Select", "0", true, true));  // Default Select option

    // Loop through the dataCollection to create and append options
    for (var i = 0; i < dataCollection.length; i++) {
        var opt = new Option(dataCollection[i].SECM_SECTIONNAME, dataCollection[i].SECM_SECTIONID);

        // Dynamically set the data-class attribute
        $(opt).attr("data-class", dataCollection[i].SECM_CM_CLASSID);

        // Append the option to the dropdown
        $(ddl).append(opt);
    }

    // Refresh the selectpicker to apply the changes
    $(ddl).selectpicker('refresh');
}
//function BindDropDownListForSection(ddl, dataCollection) {
//    $(ddl).empty();
//    $(ddl).append(new Option("Select", "0", true, true));
//    for (var i = 0; i < dataCollection.length; i++) {
//        var opt = new Option(dataCollection[i].SECM_SECTIONNAME, dataCollection[i].SECM_SECTIONID);
//        $(opt).attr("data-class", dataCollection[i].SECM_CM_CLASSID);
//        $(ddl).append(opt);
//    }
//}
//function AjaxPostForDropDownSection() {

//    //var selectedClassIds = $('#LP_ClassId').val() || [];

//    var selectedClassIds = $('#LP_ClassId').val() ;

//    var _data = JSON.stringify({
//        //ClassIds: selectedClassIds.map(Number)

//        ClassIds: selectedClassIds
//    });

//    $.ajax({
//        type: "POST",
//        url: rootDir + "JQuery/GetSectionByClassA",
//        data: _data,
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (data) {

//            // SECTION BINDING
//            BindDropDownListForSection($('#ddlLP_SectionId'), data);
//            $("#ddlLP_SectionId").selectpicker('refresh');

//            // ===============================
//            // NEW → SUBJECT LOADING HERE
//            // ===============================

//            if (selectedClassIds.length > 0) {
//                //let firstClassId = selectedClassIds[0];
//                let firstClassId = selectedClassIds;
//                AjaxPostForDropDownSubject(firstClassId);
//            } else {
//                // No class selected → clear subject dropdown
//                $("#ddlLP_SubjectId").empty()
//                    .append('<option value="0">Select Subject</option>')
//                    .selectpicker('refresh');
//            }
//        }
//    });
//}
//function AjaxPostForDropDownSection() {

//    var selectedClassIds = $('#LP_ClassId').val();

//    var _data = JSON.stringify({
//        ClassIds: selectedClassIds
//    });

//    $.ajax({
//        type: "POST",
//        url: rootDir + "JQuery/GetSectionByClassA",
//        data: _data,
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (data) {

//            // SECTION BINDING
//            BindDropDownListForSection($('#ddlLP_SectionId'), data);
//            $("#ddlLP_SectionId").selectpicker('refresh');

//            // ===============================
//            // NEW → SUBJECT LOADING HERE
//            // ===============================

//            if (selectedClassIds.length > 0) {
//                let firstClassId = selectedClassIds;
//                AjaxPostForDropDownSubject(firstClassId);
//            } else {
//                // No class selected → clear subject dropdown
//                $("#ddlLP_SubjectId").empty()
//                    .append('<option value="0">Select Subject</option>')
//                    .selectpicker('refresh');
//            }

//            // Set the `data-code` attribute based on the selected class
//            $("#LP_ClassId option").each(function () {
//                // Reset the data-code attribute for all options
//                $(this).removeAttr('data-code');

//                // Check if the current option is selected
//                if (selectedClassIds.includes($(this).val())) {
//                    // Set the data-code attribute to class name (CM_CLASSNAME)
//                    $(this).attr('data-code', $(this).text());
//                }
//            });
//        }
//    });
//}
function AjaxPostForDropDownSection() {
    var selectedClassIds = $('#LP_ClassId').val();  // Get selected class IDs

    var _data = JSON.stringify({
        ClassIds: selectedClassIds  // Prepare the data for the API
    });

    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/GetSectionByClassA",  // API endpoint
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            // Bind sections to the dropdown
            BindDropDownListForSection($('#ddlLP_SectionId'), data);

            // Refresh the selectpicker to apply the changes
            $("#ddlLP_SectionId").selectpicker('refresh');

            // ================================
            // NEW → SUBJECT LOADING HERE
            // ================================
            if (selectedClassIds.length > 0) {
                let firstClassId = selectedClassIds;
                AjaxPostForDropDownSubject(firstClassId);  // Load subjects
            } else {
                // No class selected → clear subject dropdown
                $("#ddlLP_SubjectId").empty()
                    .append('<option value="0">Select Subject</option>')
                    .selectpicker('refresh');
            }

            // Set the `data-code` attribute based on the selected class
            $("#LP_ClassId option").each(function () {
                // Ensure no option is disabled (remove any disabled attributes)
                $(this).prop('disabled', false);

                // Reset the data-code attribute for all options
                $(this).removeAttr('data-code');

                // Check if the current option is selected
                if (selectedClassIds.includes($(this).val())) {
                    // Set the data-code attribute to the class name
                    $(this).attr('data-code', $(this).text());
                }
            });

            // Refresh the selectpicker again after updating the options
            $("#LP_ClassId").selectpicker('refresh');
        }
    });
}
function loadClassesForFaculty(facultyId) {
    facultyId = parseInt(facultyId, 10);
    console.log("loadClassesForFaculty:", facultyId);

    var $classSelect = $('#LP_ClassId');

    if (isNaN(facultyId) || facultyId <= 0) {
        $classSelect.empty().append('<option value="">No classes found</option>');
        $classSelect.selectpicker('refresh');
        return;
    }

    $.ajax({
        url: '/JQuery/GetClassesByFaculty',
        type: 'GET',
        data: { facultyId: facultyId },
        success: function (resp) {
            console.log("FACULTY CLASSES RESPONSE: ", resp);

            $classSelect.empty();

            if (resp && resp.success && resp.data.length > 0) {
                $.each(resp.data, function (i, item) {
                    $classSelect.append(
                        $('<option>', { value: item.Id, text: item.Name })
                    );
                });
            } else {
                $classSelect.append('<option value="">No classes found</option>');
            }

            $classSelect.selectpicker('refresh');
        },
        error: function () {
            console.error('GetClassesByFaculty ERROR');
        }
    });
}
$(document).ready(function () {
    
    // Load notice list
    if (typeof LessonPlanList === 'function') {
        LessonPlanList();
    }

    // Load faculty wise classes on FIRST LOAD
    //var initialFacultyId = getEffectiveFacultyId();
    //console.log("INITIAL FACULTY ID:", initialFacultyId);

    //if (initialFacultyId > 0) {
    //    setTimeout(function () {
    //        loadClassesForFaculty(initialFacultyId);
    //    }, 100);
    //}

    // When faculty dropdown changes
    $(document).on('change', '#LP_FacultyID', function () {
        loadClassesForFaculty($(this).val());
    });

    // If using selectpicker
    $(document).on('changed.bs.select', '#LP_FacultyID', function () {
        loadClassesForFaculty($(this).val());
    });

    // EDIT MODE: Rebind class/section
    if ($('#LP_Id').val() && $('#LP_Id').val() > 0) {
        var selectedClasses = $('#hdnLP_ClassId').val().split(',');
        $('#LP_ClassId').val(selectedClasses);
        $('#LP_ClassId').selectpicker('refresh');

        AjaxPostForDropDownSection(function () {
            var selectedSections = $('#hdnLP_SectionId').val().split(',');
            $('#ddlLP_SectionId').val(selectedSections);
            $('#ddlLP_SectionId').selectpicker('refresh');
        });
    }
});
function AjaxPostForDropDownSubject(Id) {
    var _data = JSON.stringify({
        CSGWS_Class_Id: parseInt(Id)
    });

    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/GetGroupWiseSubject",
        data: _data,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            BindDropDownListForSubject($('#ddlLP_SubjectId'), data);
            $("#ddlLP_SubjectId").selectpicker('refresh');
        }
    });
}
//function BindDropDownListForSubject(ddl, dataCollection) {
//    ddl.empty();
//    ddl.append(new Option("Select Subject", "0", true, true));

//    for (var i = 0; i < dataCollection.length; i++) {
//        ddl.append(
//            new Option(dataCollection[i].CSGWS_SBM_SubjectName, dataCollection[i].CSGWS_Sub_Id)
//        );
//    }
//}



function BindDropDownListForSubject(ddl, dataCollection) {
    ddl.empty();
    ddl.append(new Option("Select Subject", "0", true, true));

    for (var i = 0; i < dataCollection.length; i++) {
        // Create the option element
        var option = new Option(dataCollection[i].CSGWS_SBM_SubjectName, dataCollection[i].CSGWS_Sub_Id);

        // Set the data-code attribute dynamically based on the subject name or any other property
        $(option).attr('subject-code', dataCollection[i].CSGWS_SBM_SubjectName);

        // Append the option to the dropdown
        ddl.append(option);
    }
}

function HandleSectionDropdownBasedOnClass() {

    var selectedClasses = $("#LP_ClassId").val() || [];

    if (selectedClasses.length === 1) {
        // ENABLE section dropdown
        $("#ddlLP_SectionId").prop("disabled", false);
        $("#ddlLP_SectionId").selectpicker('refresh');

        // Load section for that class
        AjaxPostForDropDownSection();
    }
    else {
        // MORE THAN ONE CLASS SELECTED → DISABLE SECTION DROPDOWN
        $("#ddlLP_SectionId").val([]);      // Clear selection
        $("#ddlLP_SectionId").prop("disabled", true);
        $("#ddlLP_SectionId").selectpicker('refresh');
    }
}
$(document).on("changed.bs.select", "#LP_ClassId", function () {
    HandleSectionDropdownBasedOnClass();
});
function parseDotNetDate(dateStr) {
    if (!dateStr) return "";

    var match = /\/Date\((\d+)\)\//.exec(dateStr);
    if (!match) return dateStr.substring(0, 10);

    var d = new Date(parseInt(match[1], 10));

    var day = ("0" + d.getDate()).slice(-2);
    var month = ("0" + (d.getMonth() + 1)).slice(-2);
    var year = d.getFullYear();

    return day + "/" + month + "/" + year;
}
function BindList() {

    $("#update-panel").html("Loading...");

    $.ajax({
        url: "/JQuery/LessonPlanListFilter",
        type: "GET",
        dataType: "json",
        data: {
            FacultyId: $("#LP_FacultyID").val(),
            SubjectId: $("#LP_SubjectId").val(),
            FromDate: convertToSqlDate($("#LP_FromDate").val()),
            ToDate: convertToSqlDate($("#LP_ToDate").val())
        },
        success: function (res) {

            if (!res || !res.Data || res.Data.length === 0) {
                $("#update-panel").html("<div>No data found</div>");
                return;
            }

            // Destroy previous DataTable
            if ($.fn.DataTable && $.fn.DataTable.isDataTable && $.fn.DataTable.isDataTable('#tblList')) {
                $('#tblList').DataTable().clear().destroy();
            }

            var html = '';
            html += '<table id="tblList" class="table table-bordered table-striped">';
            html += '<thead><tr>';
            html += '<th>SL</th><th>Faculty</th><th>Class</th><th>Section</th><th>Subject</th><th>From</th><th>To</th><th>Action</th>';
            html += '</tr></thead>';
            html += '<tbody>';

            for (var i = 0; i < res.Data.length; i++) {
                var r = res.Data[i];
                var fromDate = parseDotNetDate(r.LP_FromDate);
                var toDate = parseDotNetDate(r.LP_ToDate);

                html += '<tr>';
                html += '<td>' + (i + 1) + '</td>';
                html += '<td>' + (r.FP_Name || "") + '</td>';
                html += '<td>' + (r.ClassNames || "") + '</td>';
                html += '<td>' + (r.SectionNames || "") + '</td>';
                html += '<td>' + (r.SBM_SubjectName || "") + '</td>';
                html += '<td>' + formatJsonDate(r.LP_FromDate) + '</td>';
                html += '<td>' + formatJsonDate(r.LP_ToDate) + '</td>';

                if (res.CanDelete === true) {
                    html += '<td><button class="btn btn-danger btn-sm" onclick="Confirm(' + r.LP_Id + ')">Delete</button></td>';
                } else {
                    html += '<td><button class="btn btn-danger btn-sm" disabled>Delete</button></td>';
                }

                html += '</tr>';
            }

            html += '</tbody></table>';

            $("#update-panel").html(html);

            if ($.fn.DataTable) {
                $("#tblList").DataTable();
            }
        },
        error: function () {
            ErrorToast("Something went wrong");
        }
    });
}

function Confirm(id) {
    if (confirm("Are you sure?")) {
        Deletedata(id);
    }
}

function Deletedata(fieldId) {

    var _data = JSON.stringify({
        MainTableName: 'LessonPlan_LP',
        MainFieldName: 'LP_Id',
        PId: parseInt(fieldId, 10)
    });

    $.ajax({
        url: '/JQuery/DeleteData',
        type: 'POST',
        data: _data,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            if (data && data.IsSuccess === true) {
                SuccessToast(data.Message);
                setTimeout(function () {
                    BindList();
                }, 1000);
            } else {
                ErrorToast(data && data.Message ? data.Message : "Delete failed");
            }
        },
        error: function () {
            ErrorToast("Something wrong happened.");
        }
    });
}
